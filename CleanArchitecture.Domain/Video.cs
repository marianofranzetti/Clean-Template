﻿using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain
{
    public class Video : BaseDomainModel
    {
        public Video()
        {
            Actores = new HashSet<Actor>();
        }
        public string? Nombre { get; set; }
        public int StreamerId { get; set; }
        public virtual Streamer? Streamer { get; set; }
        public virtual ICollection<Actor> Actores { get; }
        public virtual Director Director { get; set; }

    }
}
