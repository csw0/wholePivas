namespace PivasTool
{
    public class InsertSql
    {
        protected internal string SetTools(ToolAdd ta)
        {
            if (ta.um == null)
            {
                return string.Format("Insert into Tools values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", ta.Txt_Name.Text, ta.Txt_Version.Text, ta.Comb_MaxCategories.SelectedItem.ToString(), ta.Comb_MinCategories.SelectedItem.ToString(), ta.Txt_ImgName.Text, ta.Txt_Path.Text, string.Empty);
            }
            else
            {
                return string.Format("UPDATE [dbo].[Tools] SET [ToolsName] = '{0}',[ToolsVersion] = '{1}',[ToolsMaxCategories] = '{2}',[ToolsMinCategories] = '{3}',[ToolsImgName] = '{4}',[ToolsPath] = '{5}',[ToolsDetails] = '{6}' WHERE ToolsID='{7}'", ta.Txt_Name.Text, ta.Txt_Version.Text, ta.Comb_MaxCategories.SelectedItem.ToString(), ta.Comb_MinCategories.SelectedItem.ToString(), ta.Txt_ImgName.Text, ta.Txt_Path.Text, string.Empty, ta.um.dr["ToolsID"].ToString());
            }
        }
    }
}
