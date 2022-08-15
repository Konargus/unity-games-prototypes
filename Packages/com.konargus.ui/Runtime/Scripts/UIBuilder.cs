using UnityEngine;
using UnityEngine.UI;

namespace com.konargus.ui
{
    public class UIBuilder : IUIBuilder
    {
        public (Canvas, IMenuSimple) BuildSimpleMenu()
        {
            IMenuSimple menu = Object.Instantiate(Resources.Load<MenuSimple>("MenuSimple"));
            menu.Stretch();
            
            return (CreateCanvas(), menu);
        }

        public (Canvas, IGameView) BuildGameView()
        {
            var gameView = Object.Instantiate(Resources.Load<GameView>("GameView"));
            ((IGameView) gameView).Stretch();

            gameView.SetJoystick(Object.Instantiate(Resources.Load<Joystick>("Joystick"), gameView.Transform));
            return (CreateCanvas(), gameView);
        }
        
        private static Canvas CreateCanvas()
        {
            var canvas = new GameObject("Canvas", typeof(GraphicRaycaster)).GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.TexCoord1;
            
            var canvasScaler = canvas.gameObject.AddComponent<CanvasScaler>();
            canvasScaler.matchWidthOrHeight = 0.5f;
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

            return canvas;
        }
    }
}