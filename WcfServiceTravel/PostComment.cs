﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WcfServiceTravel
{
    public class PostComment
    {
        public int Id { get; set; }
        public string comment1 { get; set; }
      
        public int ? postId { get; set; }

        public int ? UserId { get; set; }
    }
}