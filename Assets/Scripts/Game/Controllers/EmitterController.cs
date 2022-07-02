using Game.View;

namespace Game.Controllers
{
    public class EmitterController
    {
        private readonly CannonView _cannonView;
        private readonly LevelObjectView _aimView;

        public EmitterController(CannonView cannonView, LevelObjectView aimView)
        {
            _aimView = aimView;
            _cannonView = cannonView;
        }

        public void Update()
        {
            
        }
    }
}