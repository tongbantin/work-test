using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TreasuryShadowSystem.Helper;
using System.Linq.Expressions;

namespace TreasuryShadowSystem
{
    /// <summary>
    /// Summary description for adminws
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class adminws : System.Web.Services.WebService
    {
        #region Menu

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetMenuData(int? numRows, int? page, string sortField, string sortOrder, 
                                    bool isSearch, string searchField, string searchString, string searchOper)
        {
            
            DataSet ds = Model.Model_Tb_Menu.getMenus();
            DataTable dt = ds.Tables[0];
            List<Model.Model_Tb_Menu> menu = new List<TreasuryShadowSystem.Model.Model_Tb_Menu>();
            Model.Model_Tb_Menu menuItem = null;
            
            foreach (DataRow dr in dt.Rows)
            {

                menuItem = new Model.Model_Tb_Menu();
                menuItem.DataCode = dr["DATA_CODE"].ToString();
                menuItem.DataNameEng = dr["DATA_NAME_ENG"].ToString();
                menuItem.DataType = dr["DATA_TYPE"].ToString();
                menuItem.Enabled = dr["ENABLED"].ToString();
                menuItem.MenuID = dr["ID"].ToString();
                menuItem.PathUrl = dr["PATH_URL"].ToString();
                menuItem.UnderMenu = dr["PARENTID"].ToString();
                menuItem.Seq = dr["SEQ"].ToString();

                menu.Add(menuItem);
            }

            var query = (from m in menu
                        select m).AsQueryable();

            if (isSearch)
            {
                query = query.Where<Model.Model_Tb_Menu>(searchField, searchString,
                                                                    (WhereOperation)StringEnum.Parse(typeof(WhereOperation), searchOper));
            }

            //--- setup calculations
            int pageIndex = page ?? 1; //--- current page
            int pageSize = numRows ?? 10; //--- number of rows to show per page
            int totalRecords = query.Count(); //--- number of total items from query
            int totalPages = (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize); //--- number of pages

            //--- filter dataset for paging and sorting
            //IQueryable<User> orderedRecords = query.OrderBy(sortfield);
            IQueryable<Model.Model_Tb_Menu> orderedRecords = query.OrderBy(m => sortField).AsQueryable();
            //IQueryable<Model.Model_Tb_Menu> orderedRecords = query.OrderBy<Model.Model_Tb_Menu>(sortField, sortOrder);
            IEnumerable<Model.Model_Tb_Menu> sortedRecords = orderedRecords.ToList();
            if (sortOrder == "desc") sortedRecords = sortedRecords.Reverse();
            sortedRecords = sortedRecords.Skip((pageIndex - 1) * pageSize) //--- page the data
                                          .Take(pageSize);

            //--- format json
            var jsonData = new {
                                totalpages = totalPages, //--- number of pages
                                page = pageIndex, //--- current page
                                totalrecords = totalRecords, //--- total items
                                rows = (
                                    from row in sortedRecords
                                    select new
                                    {
                                        i = row.MenuID,
                                        cell = new string[] {
                                            row.MenuID, row.Seq, row.DataCode, row.DataType, row.DataNameEng, row.PathUrl, row.UnderMenu, row.Enabled
                                        }
                                    }
                               ).ToArray()
                            };

            return JsonConvert.SerializeObject(jsonData);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetMenuDataById(string Id)
        {
            DataRow dr = Model.Model_Tb_Menu.getMenu(Id);

            var jsonData = JsonConvert.SerializeObject(dr);

            return jsonData.ToString();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string PerformCRUDAction(string MenuID, string Seq, string DataCode, string DataType, string DataNameEng, string PathUrl,
                                           string UnderMenu, string Enabled, string oper, string id)
        {
            //Seq=32&DataCode=UserGroup&DataType=MENU&DataNameEng=User+Group&PathUrl=Management%2Ffrm_User.aspx&UnderMenu=21&Enabled=1&oper=edit&id=10
            bool result = false;
            ActionOperation aop = (ActionOperation)StringEnum.Parse(typeof(ActionOperation), oper);
            Model.Model_Tb_Menu menu = new TreasuryShadowSystem.Model.Model_Tb_Menu();
            menu.MenuID = MenuID; //ID.Split(',')[0];
            menu.DataCode = DataCode;
            menu.DataNameEng = DataNameEng;
            menu.DataType = DataType;
            menu.Enabled = Enabled;
            menu.PathUrl = PathUrl;
            menu.UnderMenu = UnderMenu;
            menu.Seq = Seq;

            switch (aop)
            { 
                case ActionOperation.Add:
                    if (String.Compare(id, "_empty") == 0)
                        menu.MenuID = string.Empty;
                    result = menu.InsertData();
                    break;
                case ActionOperation.Edit:
                    result = menu.UpdateData();
                    break;
                case ActionOperation.Delete:
                    result = menu.DeleteData();
                    break;
            }

            return JsonConvert.SerializeObject(result);
        }
        #endregion

        #region UserGroup
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetUSGroupData(int? numRows, int? page, string sortField, string sortOrder,
                                    bool isSearch, string searchField, string searchString, string searchOper)
        {

            DataSet ds = Model.Model_Tb_UserGroup.getUserGroups();
            DataTable dt = ds.Tables[0];
            List<Model.Model_Tb_UserGroup> usergroups = new List<TreasuryShadowSystem.Model.Model_Tb_UserGroup>();
            Model.Model_Tb_UserGroup item = null;

            foreach (DataRow dr in dt.Rows)
            {

                item = new Model.Model_Tb_UserGroup();
                item.BU = dr["BU"].ToString();
                item.DepCode = dr["DEP_CODE"].ToString();
                item.UserGroupID = dr["USERGROUP_ID"].ToString();
                item.UserGroupName = dr["USERGROUP_NAME"].ToString();

                usergroups.Add(item);
            }

            var query = (from m in usergroups
                         select m).AsQueryable();

            if (isSearch)
            {
                query = query.Where<Model.Model_Tb_UserGroup>(searchField, searchString,
                                                                    (WhereOperation)StringEnum.Parse(typeof(WhereOperation), searchOper));
            }

            //--- setup calculations
            int pageIndex = page ?? 1; //--- current page
            int pageSize = numRows ?? 10; //--- number of rows to show per page
            int totalRecords = query.Count(); //--- number of total items from query
            int totalPages = (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize); //--- number of pages

            //--- filter dataset for paging and sorting
            IQueryable<Model.Model_Tb_UserGroup> orderedRecords = query.OrderBy(m => sortField).AsQueryable();
            IEnumerable<Model.Model_Tb_UserGroup> sortedRecords = orderedRecords.ToList();
            if (sortOrder == "desc") sortedRecords = sortedRecords.Reverse();
            sortedRecords = sortedRecords.Skip((pageIndex - 1) * pageSize) //--- page the data
                                          .Take(pageSize);

            //--- format json
            var jsonData = new
            {
                totalpages = totalPages, //--- number of pages
                page = pageIndex, //--- current page
                totalrecords = totalRecords, //--- total items
                rows = (
                    from row in sortedRecords
                    select new
                    {
                        i = row.UserGroupID,
                        cell = new string[] {
                                            row.UserGroupID, row.UserGroupName, row.BU, row.DepCode
                                        }
                    }
               ).ToArray()
            };

            return JsonConvert.SerializeObject(jsonData);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string PerformCRUDGroupAction(string UserGroupID, string UserGroupName, string BU, string DepCode, string oper, string id)
        {
            //Seq=32&DataCode=UserGroup&DataType=MENU&DataNameEng=User+Group&PathUrl=Management%2Ffrm_User.aspx&UnderMenu=21&Enabled=1&oper=edit&id=10
            bool result = false;
            ActionOperation aop = (ActionOperation)StringEnum.Parse(typeof(ActionOperation), oper);
            Model.Model_Tb_UserGroup group = new TreasuryShadowSystem.Model.Model_Tb_UserGroup();
            group.UserGroupID = UserGroupID;
            group.UserGroupName = UserGroupName;
            group.BU = BU;
            group.DepCode = DepCode;

            switch (aop)
            {
                case ActionOperation.Add:
                    if (String.Compare(id, "_empty") == 0)
                        group.UserGroupID = string.Empty;
                    result = group.InsertData();
                    break;
                case ActionOperation.Edit:
                    result = group.UpdateData();
                    break;
                case ActionOperation.Delete:
                    result = group.DeleteData();
                    break;
            }

            return JsonConvert.SerializeObject(result);
        }
        #endregion

        #region Permission
        private Model.G_JSTree[] GetChildNodesMenu(string parentid, DataTable dtAssigned, out bool isSelected, out bool isUndetermind)
        {
            //List<Model.G_JSTree> G_JSTreeArray = new List<Model.G_JSTree>();
            DataSet ds = Model.Model_Tb_Menu.getMenusByParentId(parentid);
            DataTable dt = ds.Tables[0];
            Model.G_JSTree _G_JSTree = null;
            
            int count = dt.Rows.Count;
            Model.G_JSTree[] G_JSTreeArray = new Model.G_JSTree[count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                _G_JSTree = new Model.G_JSTree();
                _G_JSTree.data = dr["DATA_NAME_ENG"].ToString();
                _G_JSTree.state = "closed";
                _G_JSTree.IdServerUse = Convert.ToInt32(dr["ID"].ToString());
                _G_JSTree.children = null;
                isSelected = false;
                isSelected = false;
                if (dtAssigned != null)
                {
                    if (dtAssigned.Rows.Count > 0)
                    {
                        DataRow[] drs = dtAssigned.Select("ID = " + dr["ID"].ToString());
                        if (drs.Length > 0)
                        {
                            isSelected = true;
                            count--;
                        }
                    }
                }
                _G_JSTree.attr = new Model.G_JsTreeAttribute { id = dr["ID"].ToString(), selected = isSelected };

                G_JSTreeArray[i] = _G_JSTree;
                i++;
            }
            isUndetermind = count > 0 ? true : false;
            isSelected = count == 0 ? true : false;

            return G_JSTreeArray;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<Model.G_JSTree> GetAllNodesMenu(string id, string operation, string group)
        {
            DataSet ds = Model.Model_Tb_Menu.getMenusByParentId(id);
            DataTable dt = ds.Tables[0];
            DataSet dsGroup = null;
            DataTable dtAssigned = null;
            DataSet dsChildHome = Model.Model_Tb_Menu.getMenusByParentId("1"); //Home

            if (group != null)
            {
                dsGroup = Model.Model_Tb_Menu_Auth.GetByUserGroupID(group);
                dtAssigned = dsGroup.Tables[0];
            }

            List<Model.G_JSTree> G_JSTreeArray = new List<Model.G_JSTree>();
            bool isSelected = false;
            bool isUndetermind = false;
            foreach (DataRow dr in dt.Rows)
            {
                //default
                isUndetermind = false;
                isSelected = false;

                Model.G_JSTree _G_JSTree = new Model.G_JSTree();
                _G_JSTree.data = dr["DATA_NAME_ENG"].ToString();
                _G_JSTree.state = "closed";
                _G_JSTree.IdServerUse = Convert.ToInt32(dr["ID"].ToString());
                if (Convert.ToInt32(dr["GROUPID"].ToString()) == 1)  /*New Web Template 2017*/
                    _G_JSTree.children = GetChildNodesMenu(dr["ID"].ToString(), dtAssigned, out isSelected, out isUndetermind);
                else
                    _G_JSTree.children = null;

                //if (dtAssigned != null)
                //{
                //    if (dtAssigned.Rows.Count > 0)
                //    {
                //        DataRow[] drs = dtAssigned.Select("ID = " + dr["ID"].ToString());
                //        if (drs.Length > 0)
                //        {
                //            isSelected = true;
                //        }
                //        else
                //            isSelected = false;
                //    }
                //}
                _G_JSTree.attr = new Model.G_JsTreeAttribute { id = dr["ID"].ToString(), selected = isSelected, undetermind = isUndetermind };
                G_JSTreeArray.Add(_G_JSTree);
            }

            return G_JSTreeArray;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string PerformCRUDPermissionAction(string ids, string group)
        {
            bool result = false;
            string[] listid = ids.Split(',');
            Model.Model_Tb_Menu_Auth auth = new TreasuryShadowSystem.Model.Model_Tb_Menu_Auth();
            auth.UserGroup = group;
            auth.MenuAuth = new System.Collections.ArrayList();
            Model.Model_Tb_Menu_Auth item = null;

            //default add Home node
            item = new TreasuryShadowSystem.Model.Model_Tb_Menu_Auth();
            item.Menu = "1"; //Home
            item.UserGroup = group;
            auth.MenuAuth.Add(item);

            foreach (string id in listid)
            {
                item = new TreasuryShadowSystem.Model.Model_Tb_Menu_Auth();
                item.Menu = id;
                item.UserGroup = group;
                auth.MenuAuth.Add(item);
            }
            result = auth.UpdateData();

            return JsonConvert.SerializeObject(result);
        }
        #endregion
    }
}
