﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Core.Models
{
    public class Organization
    {
         /*
          Campos:
          id: INTEGER NOT NULL AUTO_INCREMENT
          name: VARCHAR NOT NULL
          image: VARCHAR NOT NULL
          address: VARCHAR NULLABLE
          phone: INTEGER NULLABLE
          email: VARCHAR NOT NULL
          welcomeText: TEXT NOT NULL
          aboutUsText: TEXT NULLABLE
          timestamps y softDeletes
          */

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrganizationId { get; set; }


        [Required(ErrorMessage = "El campo Name es requerido")]
        [StringLength(50)]
        public string Name { get; set; }


        [Required(ErrorMessage = "El campo Image es requerido")]
        [StringLength(100)]
        public string Image { get; set; }

        public List<Slide> Slides { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        //[Phone(ErrorMessage = "El formato del campo debe corresponder al de un número telefonico")]
        //[Range(0, 20, ErrorMessage = " El valor del campo debe ser un número entre 0 y 20")]
        public int? Phone { get; set; }

        [Required(ErrorMessage = "El campo email es requerido")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "El campo debe contener un formato de email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo welcomeText es requerido")]
        [StringLength(100)]
        public string WelcomeText { get; set; }

        [Required(ErrorMessage = "El campo aboutUsText es requerido")]
        [StringLength(100)]
        public string AboutUsText { get; set; }

        [Timestamp]
        public byte[] ChangeCheck { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(256)]
        public string FacebookUrl { get; set; }

        [StringLength(256)]
        public string LinkedinUrl { get; set; }

        [StringLength(256)]
        public string InstagramUrl { get; set; }
    }
}
