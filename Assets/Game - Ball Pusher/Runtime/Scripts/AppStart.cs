using System.Collections;
using com.konargus.camera;
using com.konargus.vfx;
using TMPro;
using UnityEngine;

namespace BallPusher
{
    public class AppStart : MonoBehaviour
    {
        [SerializeField] private SoccerBall ballPrefab;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private ParticleSystem pointer;
        [SerializeField] private GoldCoin[] goldCoins;
        [SerializeField] private Collider[] collidersToReactivate;

        private int _score;

        private void Start()
        {
            Application.targetFrameRate = 60;
            
            IBall ball = Instantiate(ballPrefab);
            ball.SetPosition(new Vector3(0, 0.6f, 0));

            CustomCamera.Instance.LookAt(ball.Transform);
            CustomCamera.Instance.Follow(ball.Transform, new Vector3(7.5f, 10, 0));
            
            var vfxFactorySimpleSmoke = new VisualEffectFactory(VisualEffectPrefabs.SimpleSmoke, transform);
            var collisionEffect = vfxFactorySimpleSmoke.CreateVisualEffect();

            ball.OnBeginDrag += () =>
            {
                pointer.transform.position = ball.Transform.position;
                var pointerVelocityOverLifetime = pointer.velocityOverLifetime;
                pointerVelocityOverLifetime.z = new ParticleSystem.MinMaxCurve(0);
                pointer.gameObject.SetActive(true);
            };
            
            ball.OnEndDrag += () =>
            {
                pointer.gameObject.SetActive(false);
            };
            
            ball.OnMouseDragging += (mousePosition) =>
            {
                var ballPos = ball.Transform.position;
                pointer.transform.position = ballPos;
                var vector = (mousePosition - ballPos).normalized;
                var pointerVelocityOverLifetime = pointer.velocityOverLifetime;
                pointerVelocityOverLifetime.z = new ParticleSystem.MinMaxCurve((mousePosition - ballPos).magnitude);
                var angle = Vector3.Angle(new Vector3(0, 0, 1000), new Vector3(vector.x, 0, vector.z));
                pointer.transform.localRotation = Quaternion.Euler(0, vector.x < 0 ? -angle : angle, 0);
            };
            
            ball.OnCollision += (col) =>
            {
                if (col.gameObject.CompareTag("IgnoreCollision"))
                {
                    return;
                }
                col.collider.isTrigger = true;
                
                var impactNormal = col.contacts[0].normal;
                ball.ScaleOnImpact(impactNormal, () =>
                {
                    collisionEffect.SetPosition(col.contacts[0].point);
                    ball.SetDirection(impactNormal);
                    var angle = Vector3.Angle(Vector3.forward, new Vector3(impactNormal.x, 0, impactNormal.z));
                    collisionEffect.SetRotation(Quaternion.Euler(0, impactNormal.x < 0 ? -angle : angle, 0));
                    collisionEffect.PlayAnimation(() =>
                    {
                        col.collider.isTrigger = false;
                    });
                });
            };

            var vfxFactorySimpleBreak = new VisualEffectFactory(VisualEffectPrefabs.SimpleBreak, transform);
            
            foreach (var goldCoin in goldCoins)
            {
                ICoin coin = goldCoin;
                var breakEffect = vfxFactorySimpleBreak.CreateVisualEffect();
                breakEffect.SetPosition(coin.Transform.position);
                coin.OnTrigger += () =>
                {
                    StartCoroutine(ScoreAnimation());
                    coin.Hide();
                    breakEffect.PlayAnimation(() =>
                    {
                        breakEffect.Destroy();
                        coin.Destroy();
                    });
                };
            }

            StartCoroutine(ReactivateCollidersCoroutine());
        }

        private IEnumerator ScoreAnimation()
        {
            for (var i = 0; i < 10; i++)
            {
                _score += 5;
                scoreText.SetText(_score.ToString());
                yield return new WaitForSeconds(0.05f);
            }
        }

        // Just to be sure :(
        private IEnumerator ReactivateCollidersCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            foreach (var col in collidersToReactivate)
            {
                if (col.isTrigger)
                {
                    col.isTrigger = false;
                }
            }

            StartCoroutine(ReactivateCollidersCoroutine());
        }
    }
}