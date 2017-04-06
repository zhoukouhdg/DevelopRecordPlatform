using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevelopRecordPlatform.Model;
using DevelopRecordPlatform.BusinessLogical.BLL;

namespace DevelopRecordPlatform.Client.Common
{
    public partial class frmSetFlightAlarmTool : Form
    {
        public frmSetFlightAlarmTool()
        {
            InitializeComponent();
        }

        private void btnGetFlightInfoList_Click(object sender, EventArgs e)
        {
            GetFlightInfoList();
        }

        private void GetFlightInfoList()
        {
            List<FlightInfo> lst = null;
            new Action(() => lst = FlightAlertManager.Instance.GetFlightInfoList()).BeginInvoke(new AsyncCallback((ar) =>
            {
                if (lst != null && lst.Count > 0)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.lstFlightInfoList.DataSource = lst.ToArray();

                        this.lstFlightInfoList.ValueMember = "FlightInfoID";
                        this.lstFlightInfoList.DisplayMember = "FlightNo";

                    }));
                }

            }), null);



        }

        private void frmSetFlightAlarmTool_Load(object sender, EventArgs e)
        {
            GetFlightInfoList();
        }




        private void button1_Click(object sender, EventArgs e)
        {
            //取消所有告警
            FlightAlertManager.Instance.CancelAllFlightAlarm(GetSelectedFlight());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //增加一个告警
            FlightAlertManager.Instance.AddOneFlightAlarm(GetSelectedFlight());
        }

        private int GetSelectedFlight()
        {
            return (int)this.lstFlightInfoList.SelectedValue;
        }
    }
}
