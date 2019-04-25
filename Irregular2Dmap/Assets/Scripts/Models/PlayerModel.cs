using Assets.Scripts.Enums;
using Assets.Scripts.Managers.Player;
using Assets.Scripts.Models.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class PlayerModel
    {
        public string Nickname { get; set; }
        public RaceEnum Race { get; set; }
        public IPlayerManager PlayerManager { get; set; }

        public PlayerModel()
        {
        }
    }
}
