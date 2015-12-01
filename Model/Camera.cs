namespace LevelEditor.Model
{
    class Camera
    {
        private float _x;
        private float _y;
        public Camera()
        {
            
        }
        void Move(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}
