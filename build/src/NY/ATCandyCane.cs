using DuckGame;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    public class ATCandyCane : AmmoType
    {
        public ATCandyCane()
        {
            bulletType = typeof(CandyCaneBullet);
            bulletSpeed = 15f;
            range = 500f;
            accuracy = 0.95f;
            bulletLength = 3f;
            //sprite = new Sprite(GetPath("Holiday/candycane"));
        }
    }
}