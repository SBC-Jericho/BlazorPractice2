using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPlayGround.Shared.DTOs
{
    public class CharacterDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = DateTime.Now;
        public string Image { get; set; } = string.Empty;
        public int TeamId { get; set; }
        public int DifficultyId { get; set; }
        public bool isReadyToFight { get; set; } = true;
    }
}
