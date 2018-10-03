namespace PivasTool
{
    public class SelectSql
    {
        protected internal string GetTools()
        {
            return "select distinct ToolsMaxCategories from Tools ";
        }
        protected internal string GetToolsMin(string ToolsMaxCategories)
        {
            return string.Format("SELECT ToolsMinCategories,COUNT(ToolsName) coun FROM [dbo].[Tools] where [ToolsMaxCategories]='{0}' group by [ToolsMinCategories] order by coun desc", ToolsMaxCategories);
        }
        protected internal string GetToolsNameDetail(string ToolsMinCategories)
        {
            return string.Format("SELECT distinct ToolsID,ToolsName,ToolsVersion,ToolsImgName,ToolsPath,[ToolsMaxCategories],[ToolsMinCategories] FROM [dbo].[Tools] where [ToolsMinCategories]='{0}'", ToolsMinCategories);
        }
    }
}
