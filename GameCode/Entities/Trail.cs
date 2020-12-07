using Engine.Entity;

namespace GameCode.Entities
{
    class Trail : GameEntity
    {
        private int timeCount = 0;

        public override void Update()
        {
            timeCount++;

            if(timeCount >= 300)
            {
                Transparency -= 0.01f;

                if (Transparency <= 0)
                {
                    Destroy = true;
                }
            }
        }
    }
}
