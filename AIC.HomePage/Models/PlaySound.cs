using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace AIC.HomePage.Models
{
    class PlaySound
    {
        private static SoundPlayer player = new SoundPlayer();
        public static void Play(string soundName)
        {
            player.SoundLocation = soundName;
            player.Load();
            player.Play();
        }
    }
}
