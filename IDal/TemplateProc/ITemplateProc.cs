namespace EasyOne.IDal.TemplateProc
{
    using System;
    using System.Text;

    public interface ITemplateProc
    {
        StringBuilder GetTemplate(string project, int type, int id);
    }
}

