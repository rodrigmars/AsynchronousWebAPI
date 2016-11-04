using System.Collections.Generic;
using WorldMusic.Domain.Entities;

namespace UnitTestWorldMusic.MockData
{
    public class MoqMusic
    {
        public static IEnumerable<Music> MoqMusics
        {
            get
            {
                return new List<Music>{
                    new Music{ Title = "1. Allure-2521", Track = 1, IsActive = true, IDProcess = 2521},
                    new Music{ Title = "2. Evolution-2522", Track = 2, IsActive = true,IDProcess = 2522},
                    new Music{ Title = "3. Idle Minds-2523", Track = 3, IsActive = false,IDProcess = 2523},
                    new Music{ Title = "4. Miracle-2524", Track = 4, IsActive = true,IDProcess = 2524},
                    new Music{ Title = "5. Halogen-2525", Track = 5, IsActive = true,IDProcess = 2525},
                    new Music{ Title = "6. New Devil-2526", Track = 6, IsActive = true,IDProcess = 2526},
                    new Music{ Title = "7. Patience-2527", Track = 7, IsActive = true,IDProcess = 2527},
                    new Music{ Title = "8. Guiding Lights-2528", Track = 8, IsActive = true, IDProcess = 2528},
                    new Music{ Title = "9. Kaikoma-2529", Track = 9, IsActive = true, IDProcess = 2529},
                    new Music{ Title = "10. The Constant-2530", Track = 10, IsActive = true, IDProcess = 2530},
                };
            }
        }
    }
}
