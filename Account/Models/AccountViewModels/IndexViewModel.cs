﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Aiursoft.Account.Models.AccountViewModels
{
    public class IndexViewModel : AccountViewModel
    {
        [Obsolete(error: true, message: "This method is only for framework!")]
        public IndexViewModel() { }
        public IndexViewModel(AccountUser user) : base(user, 0, "Profile")
        {
            Recover(user);
        }
        public void Recover(AccountUser user)
        {
            base.Recover(user, 0, "Profile");
            NickName = user.NickName;
            Bio = user.Bio;
            Bio2 = user.Bio;
        }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Nickname")]
        public virtual string NickName { get; set; }
        [MaxLength(80)]
        [Display(Name = "Bio")]
        public virtual string Bio { get; set; }
        [MaxLength(80)]
        [Display(Name = "Bio2")]
        public virtual string Bio2 { get; set; }
    }
}
