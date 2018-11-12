namespace EasyOne.WebSite.Controls.Crm
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Crm;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class PersonalInformation : BaseUserControl
    {

        public void GetContacter(ContacterInfo contacterInfo)
        {
            contacterInfo.Birthday = DataConverter.CDate(this.DpkBirthday.Text, null);
            contacterInfo.IdCard = this.TxtIDCard.Text;
            contacterInfo.NativePlace = this.TxtNativePlace.Text;
            contacterInfo.Nation = this.TxtNation.Text;
            contacterInfo.Sex = (UserSexType) Enum.Parse(typeof(UserSexType), this.RadlSex.SelectedValue);
            contacterInfo.Marriage = (UserMarriageType) Enum.Parse(typeof(UserMarriageType), this.RadlMarriage.SelectedValue);
            contacterInfo.Education = DataConverter.CLng(this.DropEducation.SelectedValue);
            contacterInfo.GraduateFrom = this.TxtGraduateFrom.Text;
            contacterInfo.InterestsOfLife = this.TxtInterestsOfLife.Text;
            contacterInfo.InterestsOfCulture = this.TxtInterestsOfCulture.Text;
            contacterInfo.InterestsOfAmusement = this.TxtInterestsOfAmusement.Text;
            contacterInfo.InterestsOfSport = this.TxtInterestsOfSport.Text;
            contacterInfo.InterestsOfOther = this.TxtInterestsOfOther.Text;
            contacterInfo.Income = DataConverter.CLng(this.DropIncome.SelectedValue);
            contacterInfo.Family = this.TxtFamily.Text;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && (this.DropIncome.Items.Count == 0))
            {
                Choiceset.DropDownListDataBind("PE_Contacter", "InCome", this.DropIncome);
                Choiceset.DropDownListDataBind("PE_Contacter", "Education", this.DropEducation);
            }
        }

        public void SetContacter(ContacterInfo contacterInfo)
        {
            if (contacterInfo.Birthday.HasValue)
            {
                this.DpkBirthday.Text = contacterInfo.Birthday.Value.ToString("yyyy-MM-dd");
            }
            this.TxtIDCard.Text = contacterInfo.IdCard;
            this.TxtNativePlace.Text = contacterInfo.NativePlace;
            this.TxtNation.Text = contacterInfo.Nation;
            this.RadlSex.SelectedValue = contacterInfo.Sex.ToString("D");
            this.RadlMarriage.SelectedValue = contacterInfo.Marriage.ToString("D");
            this.TxtGraduateFrom.Text = contacterInfo.GraduateFrom;
            this.TxtInterestsOfLife.Text = contacterInfo.InterestsOfLife;
            this.TxtInterestsOfCulture.Text = contacterInfo.InterestsOfCulture;
            this.TxtInterestsOfAmusement.Text = contacterInfo.InterestsOfAmusement;
            this.TxtInterestsOfSport.Text = contacterInfo.InterestsOfSport;
            this.TxtInterestsOfOther.Text = contacterInfo.InterestsOfOther;
            this.TxtFamily.Text = contacterInfo.Family;
            Choiceset.DropDownListDataBind("PE_Contacter", "Education", this.DropEducation, contacterInfo.Education);
            Choiceset.DropDownListDataBind("PE_Contacter", "Income", this.DropIncome, contacterInfo.Income);
        }

        public UserSexType UserSex
        {
            get
            {
                return (UserSexType) DataConverter.CLng(this.RadlSex.SelectedValue);
            }
        }
    }
}

