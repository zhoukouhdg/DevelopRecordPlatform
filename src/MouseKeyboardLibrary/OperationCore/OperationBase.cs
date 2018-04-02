using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperationCore
{
    public abstract class OperationBase
    {
        /// <summary>
        /// 操作后等待间隔时间
        /// </summary>
        public int StayInterval
        {
            //get { return this.StayInterval > 0 ? this.StayInterval : 1000; }
            //set { this.StayInterval = value; }
            get; set;
        } = 500;

        /// <summary>
        /// 操作后是否等待
        /// </summary>
        public bool IsStay { get; set; } = true;


        public OperationBase()
        {

        }

        /// <summary>
        /// 虚方法：初始化
        /// </summary>
        public virtual void Init()
        {
            Console.WriteLine("初始化");
        }

        /// <summary>
        /// 虚方法：执行操作
        /// </summary>
        public virtual void ExecuteOperate()
        {
            Console.WriteLine("执行操作");
        }
    }
}
