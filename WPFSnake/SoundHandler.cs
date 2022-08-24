using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;
using System.Diagnostics;

namespace WPFSnake
{
    class SoundHandler
    {

        public void playDefaultSound(String name)
        {
            Trace.WriteLine(Directory.GetCurrentDirectory());
            
            SoundPlayer player = new SoundPlayer("../../../sounds/" + name + ".wav");
            player.Load();
            player.Play();
        }


        public void playSoungWithPath(String path)
        {
            SoundPlayer player = new SoundPlayer(path);
            player.Load();
            player.Play();
        }


    }

}
