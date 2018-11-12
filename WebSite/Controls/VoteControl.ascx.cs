namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Model;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class VoteControl : BaseUserControl
    {
        private int generalId;

        public void Add(int id)
        {
            VoteInfo voteInfoByGeneralId = Votes.GetVoteInfoByGeneralId(this.generalId);
            voteInfoByGeneralId.GeneralId = id;
            voteInfoByGeneralId.VoteTitle = this.TxtVoteTitle.Text;
            voteInfoByGeneralId.IsAlive = this.ChkIsAlive.Checked;
            voteInfoByGeneralId.StartTime = DataConverter.CDate(this.DpkStartTime.Text);
            voteInfoByGeneralId.EndTime = DataConverter.CDate(this.DpkEndTime.Text);
            voteInfoByGeneralId.VoteItem = this.GetItems();
            voteInfoByGeneralId.ItemType = DataConverter.CLng(this.DropItemType.SelectedValue);
            if (voteInfoByGeneralId.IsNull && this.ChkIsAlive.Checked)
            {
                Votes.Add(voteInfoByGeneralId);
            }
            else
            {
                Votes.Update(voteInfoByGeneralId);
            }
        }

        private string GetItems()
        {
            IList<VoteItemInfo> list = new List<VoteItemInfo>();
            for (int i = 1; i < 9; i++)
            {
                TextBox box = this.FindControl("TxtItem" + i.ToString()) as TextBox;
                TextBox box2 = this.FindControl("TxtVoteNumber" + i.ToString()) as TextBox;
                if ((box != null) && (box2 != null))
                {
                    VoteItemInfo item = new VoteItemInfo();
                    item.Title = box.Text;
                    item.VoteNumber = DataConverter.CLng(box2.Text);
                    list.Add(item);
                }
            }
            Serialize<VoteItemInfo> serialize = new Serialize<VoteItemInfo>();
            return serialize.SerializeFieldList(list);
        }

        private void InitControl()
        {
            if (!base.IsPostBack && (this.generalId > 0))
            {
                VoteInfo voteInfoByGeneralId = Votes.GetVoteInfoByGeneralId(this.generalId);
                if (!voteInfoByGeneralId.IsNull)
                {
                    this.ChkIsAlive.Checked = voteInfoByGeneralId.IsAlive;
                    this.TxtVoteTitle.Text = voteInfoByGeneralId.VoteTitle;
                    this.DpkStartTime.Text = voteInfoByGeneralId.StartTime.ToString();
                    this.DpkEndTime.Text = voteInfoByGeneralId.EndTime.ToString();
                    this.DropItemType.SelectedValue = voteInfoByGeneralId.ItemType.ToString();
                    this.InitItem(voteInfoByGeneralId.VoteItem);
                }
                else
                {
                    this.DpkEndTime.Text = DateTime.Now.AddMonths(1).ToString();
                    this.DpkStartTime.Text = DateTime.Now.ToString();
                }
            }
        }

        private void InitItem(string items)
        {
            IList<VoteItemInfo> list = new Serialize<VoteItemInfo>().DeserializeFieldList(items);
            int num = 1;
            foreach (VoteItemInfo info in list)
            {
                TextBox box = this.FindControl("TxtItem" + num.ToString()) as TextBox;
                TextBox box2 = this.FindControl("TxtVoteNumber" + num.ToString()) as TextBox;
                if (box != null)
                {
                    box.Text = info.Title;
                }
                if (box2 != null)
                {
                    box2.Text = info.VoteNumber.ToString();
                }
                num++;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.generalId = BaseUserControl.RequestInt32("GeneralId", 0);
            this.InitControl();
        }
    }
}

