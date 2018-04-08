using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneKeyLogin
{
    public class GameAccount
    {
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return string.Format("UserName:{0}  Password:{1}", this.UserName, this.Password, this.IsLogin);
        }
    }
}
