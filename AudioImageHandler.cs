using System;
using System.IO;
using System.Media;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;


namespace CyberSecurityBot
{
    // Subclass for handling audio and image display
    public class AudioImageHandler
    {
        // Plays a welcome audio file at startup
        public void PlayWelcomeAudio()
        {
            string fullLocation = AppDomain.CurrentDomain.BaseDirectory;
            string newPath = fullLocation.Replace("bin\\Debug\\", "");

            try
            {
                string fullPath = Path.Combine(newPath, "HumeAI_voice-preview_Voice.wav");
                using (SoundPlayer play = new SoundPlayer(fullPath))
                {
                    play.PlaySync();
                }
            }
            catch (Exception error)
            {
                Console.Write(error.Message);
            }
        }

        // Displays an ASCII art version of a logo image
        public void DisplayLogo()
        {
            string paths = AppDomain.CurrentDomain.BaseDirectory;
            string newPath = paths.Replace("bin\\Debug\\", "");
            string fullPath = Path.Combine(newPath, "robots.jpg");

            try
            {
                Bitmap logo = new Bitmap(fullPath);
                logo = new Bitmap(logo, new Size(120, 50));

                // Converts image pixels to ASCII characters and prints to the console
                for (int height = 0; height < logo.Height; height++)
                {
                    for (int width = 0; width < logo.Width; width++)
                    {
                        Color pixelColor = logo.GetPixel(width, height);
                        int gray = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                        char asciiChar = gray > 250 ? '.' : gray > 150 ? '*' : gray > 100 ? 'o' : gray > 50 ? '#' : '@';
                        Console.Write(asciiChar);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error displaying logo: " + ex.Message);
            }
        }
    }
}