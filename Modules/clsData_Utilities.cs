using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Drawing.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Sockets;
using System.Net;
using System.Data.SqlTypes;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;

namespace RogStock2025_Utilities.Modules
{
    internal static class clsData_Utilities
    {
        //SQL server custom error handling
        static string CNST_STR_ERRORFILEPATH = "";
        static Dictionary<int, string> dicSQLErrors = new Dictionary<int, string>();

        //data source = server name
        public static readonly string CNST_STR_ODBC = "Data Source=DESKTOP-694Q8HR;Initial Catalog=RogStock;Persist Security Info=True;User ID=sa;Password=RogSQLServer1;TrustServerCertificate=true";
        //tables
        public static readonly string CNST_STR_LOCTRN = "Loc_TRN";

        public static readonly string CNST_STR_LOGIN = "Login";
        public static readonly string CNST_STR_LOGINCURRENT = "Login_Current";

        public static readonly string CNST_STR_LOTTRN = "Lot_TRN";

        public static readonly string CNST_STR_MENU_GROUPS = "Menu_Groups";
        public static readonly string CNST_STR_MENU_MENUITEMS= "Menu_MenuItems";
        public static readonly string CNST_STR_MENU_AREAS = "Menu_Areas";
        public static readonly string CNST_STR_MENU_USERSGROUPS = "Menu_UsersGroups";

        public static readonly string CNST_STR_STOCK_LOC = "Stock_Loc";
        public static readonly string CNST_STR_STOCK_LOT = "Stock_Lot";
        public static readonly string CNST_STR_STOCK_ITEMS = "Stock_Items";
        public static readonly string CNST_STR_STOCK_VENDORS = "Stock_Vendors";
        public static readonly string CNST_STR_STOCK_DESCRIPTION = "Stock_Description";
        public static readonly string CNST_STR_STOCK_MEDIA = "Stock_Media";
        public static readonly string CNST_STR_STOCK_PRODUCTFAMILY = "Stock_ProductFamily";
        public static readonly string CNST_STR_STOCK_UOM = "Stock_UOM";

        //struct typPassword
        //{
        //    public int intLength;
        //    public List<char> aryContents;
        //    public List<int> aryOrder;
        //}
        //used in current login functions
        private static string strLoggedInUser = "";
        private static string strLoggedInIP = "";

        //private static string EncryptPassword(string strPassword)
        //{
        //    /*
        //     Created 14/02/2025 By Roger Williams

        //     encrypts passed password

        //     VARS

        //     strPassword   - password

        //     returns encrypted password
        //    */

        //    int intNum = 0;
        //    char chrTemp;
        //    typPassword typPwd;
        //    int intLoc = 0;
        //    string strTemp = strPassword;
        //    byte[] arybyteTemp;

        //    //store password length
        //    typPwd.intLength = strTemp.Length;
        //    typPwd.aryContents = new List<char>();

        //    //store chars "as is"
        //    for (intNum = 0; intNum != strTemp.Length;intNum++)
        //    {
        //        typPwd.aryContents.Add(strTemp[intNum]);

        //    }
        //    //resize the order array to match password length
        //    typPwd.aryOrder = new List<int>(intNum);

        //    //encrypt the password by sorting the array by ASC order ASCII value
        //    intNum = 0;
        //    //get ASCII values for the string
        //    arybyteTemp =  Encoding.ASCII.GetBytes(strTemp);

        //    while (intNum != typPwd.aryContents.Count +1)
        //    {
        //        if (intNum != typPwd.aryContents.Count)
        //        {
        //            if (arybyteTemp[intNum] > arybyteTemp[intNum+1]) 
        //            {
        //                chrTemp = typPwd.aryContents[intNum + 1];
        //                typPwd.aryContents[intNum + 1] = typPwd.aryContents[intNum];
        //                typPwd.aryContents[intNum] = chrTemp;
        //                //reset counter
        //                intNum = 1;
        //            }
        //            else
        //            {
        //                intNum++;
        //            }
        //        }
        //        else
        //        {
        //            intNum++;
        //        }
        //    }

        //    intNum = 0;

        //    //store location of where characters where in original string
        //    while (intNum != typPwd.aryContents.Count+1)
        //    {
        //        intLoc = strTemp.IndexOf(typPwd.aryContents[intNum]);
        //        //erase char from strTemp
        //        strTemp.Remove(intLoc, 1);
        //        strTemp.Insert(intLoc, " ");
        //        typPwd.aryOrder.Add(intLoc);
        //        intNum++;
        //    }

        //    //return encrypted string
        //    strTemp = typPwd.aryContents.ToString();
        //    return strTemp;
        //}
        //private static string DecryptPassword(string strPassword)
        //{
        //    /*
        //     Created 14/02/2025 By Roger Williams

        //     decrypts passed password

        //     VARS

        //     strPassword   - password

        //     returns unencrypted password
        //    */

        //    return "w";
         
        //}


        public static bool InitCustomErrorhandler(string strPath)
        {
            /*
             Created 02/07/2025 By Roger Williams

             Inits custom SQL error resource file path variabel
             Then loads it into dictionary: dicSQLErrors

             Checks if passed path is null or file does not exist

             VAR

             strpath    - location of resource file

             RETURNS

             true if ok

            */

            StreamReader strmTemp = null;
            string strTemp = "";
            string strError = "";
            string strMsg = "";

            if (strPath.Length == 0 || !File.Exists(strPath))
            {
                return false;
            }

            CNST_STR_ERRORFILEPATH = strPath;

            strmTemp = new StreamReader(CNST_STR_ERRORFILEPATH);

            while (!strmTemp.EndOfStream)
            {
                strTemp = strmTemp.ReadLine();
                //split into error number and error message

                //purposely add,
                strError = strTemp.Substring(0, strTemp.IndexOf(",") + 1);
                strMsg = strTemp.Remove(0, strError.Length);
                //remove it
                strError = strError.Remove(strError.Length - 1);

                dicSQLErrors.Add(Convert.ToInt32(strError), strMsg);
            }

            strmTemp.Close();
            strmTemp.Dispose();
            return true;
        }

        public static string GetPassword(string strUser)
        {
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            string strTemp;

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SP_GetPassword";
                    SQLCmd.CommandType = CommandType.StoredProcedure;
                    SQLCmd.Parameters.Add("@User", SqlDbType.VarChar, 30).Value = strUser;
                    SQLCmd.Parameters.Add("@Password", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                    SQLCmd.Parameters.Add("@ErrorCustom", SqlDbType.Int).Direction = ParameterDirection.Output;
                    SQLCmd.ExecuteNonQuery();

                    if (Convert.ToInt32(SQLCmd.Parameters["@ErrorCustom"].Value) == 0)
                    {
                        return (SQLCmd.Parameters["@Password"].Value.ToString());
                    }
                    else
                    {
                        //show error to user
                        dicSQLErrors.TryGetValue(Convert.ToInt32(SQLCmd.Parameters["@ErrorCustom"].Value), out strTemp);
                        strTemp = SQLCmd.Parameters["@ErrorCustom"].Value.ToString() + "\n\n" + strTemp;
                        MessageBox.Show("Error: \n\n" + strTemp);

                        //return nothing to signify error to calling procedure
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public static bool CheckLoginExists(string strUser)
        {
            /*
             Created 18/06/2025 By Roger Williams

             checks passed user exists using: SP_CheckLoginExists
          
             VARS

             strUser       - user name


            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            bool blnOk = false;
            string strTemp = "";

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    //      SQLCmd.CommandText = "SELECT * FROM " + CNST_STR_LOGIN + " WHERE LOG_User ='" + strUser + "';";
                    //      SQLCmd.CommandType = CommandType.Text;

                    SQLCmd.CommandText = "SP_CheckLoginExists";
                    SQLCmd.CommandType = CommandType.StoredProcedure;
                    SQLCmd.Parameters.Add("@User", SqlDbType.VarChar, 30).Value = strUser;
                    SQLCmd.Parameters.Add("@ErrorCustom", SqlDbType.Int).Direction = ParameterDirection.Output;
                    SQLCmd.ExecuteNonQuery();

                    if (Convert.ToInt32(SQLCmd.Parameters["@ErrorCustom"].Value) == 0)
                    {
                        blnOk = true;
                    }
                    else
                    {
                        //dont tel user be silent as this function only needs to say yah or nah!
                        blnOk = false;
                    }

                    SQLConn.Close();
                    return blnOk;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckLogin(string strUser, string strPassword)
        {
            /*
             Created 14/02/2025 By Roger Williams

             checks passed user and password are correct

             VARS

             strUser       - user name
             strPassword   - password


            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;
            string strTemp = "";
            bool blnOk = false;
            
            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT * FROM " + CNST_STR_LOGIN + " WHERE LOG_User ='" + strUser + "';";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    if (SQLRead.Read())
                    {
                        //get password
                        strTemp = SQLRead["LOG_Password"].ToString();
                        //decrypt
                        //    strTemp = EncryptPassword(strTemp);
                        //compare with strPassword
                        if (strTemp == strPassword)
                        {
                            blnOk = true;
                        }
                        else
                        {
                            blnOk = false;
                        }
                    }
                    else
                    {
                        blnOk = false;
                    }

                    SQLRead.Close();
                    SQLConn.Close();
                    return blnOk;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n"+ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void CreateCurrentLoginRecord(string strUser)
        {
            /*
             Created 18/02/2025 By Roger Williams

             creates user logged in record in login_current


             VARS
            
             struser    - name of user

             stores struser in var strLoggedInUser for use by other functions

            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;

               string GetLocalIP()
               {
                /*
                 Created 18/02/2025 By Roger Williams

                 Gets PCs IP address

                 Modified VB code copied from the internet!

                */
                string strIP = "";
                string strHostName = "";
                IPHostEntry IPHost;

                strHostName = Dns.GetHostName();
                IPHost = Dns.GetHostEntry(strHostName);

                foreach (IPAddress IPATemp in IPHost.AddressList)
                {
                    //look for IP4 address only
                    if (IPATemp.AddressFamily == AddressFamily.InterNetwork)
                    {
                        strIP = IPATemp.ToString();
                        //store for later use
                        strLoggedInIP = IPATemp.ToString();
                        return strIP;
                    }
                }
                return strIP;
               }

            try
            {
                //store for later use elsewhere
                strLoggedInUser = strUser;

                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "INSERT INTO " + CNST_STR_LOGINCURRENT + " (LOGC_User, LOGC_PCIP)  VALUES ('" + strUser + "','" + GetLocalIP() + "');";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLCmd.ExecuteNonQuery();
                    SQLConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void DeleteCurrentLoginRecord()
        {
            /*
              Created 18/02/2025 By Roger Williams

              deletes user logged in record in login_current

              uses var strLoggedInUser for delete
             */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            try
            { 
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "DELETE FROM " + CNST_STR_LOGINCURRENT + " WHERE LOGC_User = '" + strLoggedInUser + "' AND LOGC_PCIP = '" + strLoggedInIP + "';";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLCmd.ExecuteNonQuery();
                    SQLConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //public static bool CheckForChanges(DataSet DSTTemp, string strTable)
        //{
        //    /*
        //      Created 19/02/2025 By Roger Williams

        //      Checks dataset table for changes returns true if modified

        //      DSTTemp   - dataset to work with
        //      STRTable  - table to work with

        //    */

        //    DSTTemp.AcceptChanges();

        //    if (DSTTemp.Tables[strTable].GetChanges() != null)
        //    {
        //        return true;
        //    }
        //    else
        //    { 
        //        return false;
        //    }
        //}
        //public static void ClearTable(DataSet DSTTemp, string STRTable)
        //{
        //    /*
        //      Created 19/02/2025 By Roger Williams

        //      Clears the dataset table contentsrd

        //      VARS

        //      DSTTemp   - dataset to work with
        //      STRTable  - table to work with

        //    */

        //    DSTTemp.Tables[STRTable].Clear();
        //}
        //public static void CreateNewRecord(DataSet DSTTemp, string STRTable, string strPrimaryKey, string strPrimaryKeyValue)
        //{
        //    /*
        //      Created 19/02/2025 By Roger Williams

        //      Creates new record in dataset table 

        //      VARS

        //      DSTTemp            - dataset to work with
        //      STRTable           - table to work with
        //      STRPrimaryKey      - primary key field name
        //      STRPrimaryKeyValue - primary key field name
        //    */
        //    DataRow DARRow;

        //    DARRow = DSTTemp.Tables[STRTable].NewRow();
        //    //set primary key value
        //    DARRow[strPrimaryKey] = strPrimaryKeyValue;

        //    DSTTemp.Tables[STRTable].Rows.Clear();
        //    DSTTemp.Tables[STRTable].Rows.Add(DARRow);
        //}

        public static bool CheckGroupExists(string strGroup)
        {
            /*
             Created 83/07/2025 By Roger Williams

             checks passed group exists

             VARS

             strgroup      - group name


            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            bool blnOk = false;

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT * FROM " + CNST_STR_MENU_GROUPS + " WHERE GRP_Group ='" + strGroup + "';";
                    SQLCmd.CommandType = CommandType.Text;

                    if (SQLCmd.ExecuteScalar() != null)
                    {
                        blnOk = true;
                    }
                    else
                    {
                        //dont tel user be silent as this function only needs to say yah or nah!
                        blnOk = false;
                    }

                    SQLConn.Close();
                    return blnOk;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckMenuItemExists(string strItem)
        {
            /*
             Created 84/07/2025 By Roger Williams

             checks passed menu exists

             VARS

             strgroup      - menu item name


            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            bool blnOk = false;

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT * FROM " + CNST_STR_MENU_MENUITEMS+ " WHERE MNU_MenuItemName ='" + strItem + "';";
                    SQLCmd.CommandType = CommandType.Text;

                    if (SQLCmd.ExecuteScalar() != null)
                    {
                        blnOk = true;
                    }
                    else
                    {
                        //dont tel user be silent as this function only needs to say yah or nah!
                        blnOk = false;
                    }

                    SQLConn.Close();
                    return blnOk;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckUserGroupExists(string strUserGroup)
        {
            /*
             Created 07/07/2025 By Roger Williams

             checks passed usergroup exists 
          
             VARS

             strUserGroup       - usergroup name


            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            bool blnOk = false;
            string strTemp = "";

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT * FROM " + CNST_STR_MENU_USERSGROUPS + " WHERE USRGRP_Group ='" + strUserGroup + "';";
                    SQLCmd.CommandType = CommandType.Text;

                    if (SQLCmd.ExecuteScalar() != null )
                    {
                        blnOk = true;
                    }
                    else
                    {
                        //dont tel user be silent as this function only needs to say yah or nah!
                        blnOk = false;
                    }

                    SQLConn.Close();
                    return blnOk;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckSectionExists(string strArea)
        {
            /*
             Created 83/07/2025 By Roger Williams

             checks passed area exists

             VARS

             strarea      - area name


            Note; calles checkSECTIONexists as in future sales system will have sales area - removes confusion!

            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            bool blnOk = false;

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT * FROM " + CNST_STR_MENU_AREAS + " WHERE SEC_Area ='" + strArea + "';";
                    SQLCmd.CommandType = CommandType.Text;

                    if (SQLCmd.ExecuteScalar() != null)
                    {
                        blnOk = true;
                    }
                    else
                    {
                        //dont tel user be silent as this function only needs to say yah or nah!
                        blnOk = false;
                    }

                    SQLConn.Close();
                    return blnOk;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckForLotsForItemID(string strItemID)
        /*
             Created 04/03/2025 By Roger Williams
         
             Returns true if ANY lot records exist for passed item ID

        */
        {
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADLOTStock;
            DataSet DSTLOTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_LOT + " WHERE LOT_ItemID = '" + strItemID + "';", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    intRows = DADLOTStock.Fill(DSTLOTStock, CNST_STR_STOCK_LOT);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckForStockItems()
        /*
             Created 26/02/2025 By Roger Williams
         
             If no stock items no point opening the form!

        */
        {
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {

                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_ITEMS + ";", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    DADStock.Fill(DSTStock, CNST_STR_STOCK_ITEMS);

                    //check stock item record exist
                    if (DSTStock.Tables[CNST_STR_STOCK_ITEMS].Rows.Count == 0)
                    {
                        {
                            MessageBox.Show("Cannot Edit locations As No Stock Items Created!", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public static bool CheckForLocations()
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns the true if passed location has locoation records

                 Note: this function used by location rename

            */

            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADLOTStock;
            DataSet DSTLOTStock;


            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_LOC + ";", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    DADLOTStock.Fill(DSTLOTStock, CNST_STR_STOCK_LOC);

                    //return loc/lot tracking setting
                    return DSTLOTStock.Tables[CNST_STR_STOCK_LOC].Rows.Count != 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return false;
        }
        public static bool CheckLotsForLocation(string strLocation)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns the true if passed location has lot records

                 Note: this function used by location rename

            */

            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADLOTStock;
            DataSet DSTLOTStock;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_LOT + " WHERE LOT_Location ='" + strLocation + "';", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    DADLOTStock.Fill(DSTLOTStock, CNST_STR_STOCK_LOT);

                    //return loc/lot tracking setting
                    return DSTLOTStock.Tables[CNST_STR_STOCK_LOT].Rows.Count != 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return false;
        }
        public static bool CheckForLotTracking(string strItemID)
        /*
             Created 28/02/2025 By Roger Williams
         
             Returns the loc/lot tracked value for the item

        */
        {
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADLOTStock;
            DataSet DSTLOTStock;


            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_ITEMS + " WHERE STKI_ItemID ='" + strItemID + "';", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    DADLOTStock.Fill(DSTLOTStock, CNST_STR_STOCK_ITEMS);

                    //return loc/lot tracking setting
                    return (bool)DSTLOTStock.Tables[CNST_STR_STOCK_ITEMS].Rows[0]["STKI_LocLot"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckForAreas()
        /*
             Created 07/07/2025 By Roger Williams
         
             Returns true if ANY area records exist

        */
        {
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADLOTStock;
            DataSet DSTLOTStock;
            int intRows = 0;


            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_MENU_AREAS + ";", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    intRows = DADLOTStock.Fill(DSTLOTStock, CNST_STR_MENU_AREAS);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckAreaExists(string strArea)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns true if area record for passed area

            */
            SqlConnection SQLConnArea;
            SqlCommand SQLCmdArea;
            SqlDataAdapter DADArea;
            DataSet DSTArea;
            int intRows = 0;

            try
            {
                using (SQLConnArea = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnArea.Open();
                    //load Area items
                    SQLCmdArea = new SqlCommand("SELECT * FROM " + CNST_STR_MENU_AREAS + " WHERE SEC_Area = '" + strArea + "';", SQLConnArea);
                    DADArea = new SqlDataAdapter(SQLCmdArea);
                    DSTArea = new DataSet();
                    intRows = DADArea.Fill(DSTArea, CNST_STR_MENU_AREAS);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckForLots()
        /*
             Created 04/03/2025 By Roger Williams
         
             Returns true if ANY lot records exist

        */
        {
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADLOTStock;
            DataSet DSTLOTStock;
            int intRows = 0;


            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_LOT + ";", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    intRows = DADLOTStock.Fill(DSTLOTStock, CNST_STR_STOCK_LOT);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckProdFamExists(string strProdFam)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns true if ANY product family records exist with passed product family

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;


            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_PRODUCTFAMILY + " WHERE STKP_ProductFamily = '" + strProdFam + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, CNST_STR_STOCK_PRODUCTFAMILY);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool CheckStockItemIDExists(string strItemID)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns true if ANY stock item records exist with passed item ID

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_ITEMS + " WHERE STKI_ItemID = '" + strItemID + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, CNST_STR_STOCK_ITEMS);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool CheckLocationExists(string strLocation)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns true if ANY stock loc records exist with passed location

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_LOC + " WHERE LOC_Location = '" + strLocation + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, CNST_STR_STOCK_LOC);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool CheckForProductFamily()
        {
            /*
                 Created 07/03/2025 By Roger Williams

                 Returns true if ANY stock prod fam records exist 

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_PRODUCTFAMILY + ";", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, CNST_STR_STOCK_PRODUCTFAMILY);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool CheckForUOM()
        {
            /*
                 Created 07/03/2025 By Roger Williams

                 Returns true if ANY stock UOM records exist 

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {

                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_UOM + ";", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, CNST_STR_STOCK_UOM);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckUOMExists(string strUOM)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns true if ANY stock UOM records exist with passed UOM

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_UOM + " WHERE STKU_Desc = '" + strUOM + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, CNST_STR_STOCK_UOM);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }




        //*****************other*****************
        public static int GetMenuItemsCountForArea(string strArea)
        {
            /*
             Created 07/07/2025 By Roger Williams

             returns numbver of records in Menu_MenuItems that have passed area  
          
             VARS

             strArea       - area


            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            bool blnOk = false;
            int intNum = 0;


            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT  COUNT (*) FROM " + CNST_STR_MENU_MENUITEMS + " WHERE MNU_DisplayWhere ='" + strArea + "';";
                    SQLCmd.CommandType = CommandType.Text;

                    intNum =(int) SQLCmd.ExecuteScalar();

                    SQLConn.Close();
                    return intNum;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return intNum;
            }
        }


        public static int GetMenuItemsCountForGroup(string strGroup)
        {
            /*
             Created 07/07/2025 By Roger Williams

             returns numbver of records in Menu_MenuItems that have passed group
          
             VARS

             strGroup       - group


            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            bool blnOk = false;
            int intNum = 0;


            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT  COUNT (*) FROM " + CNST_STR_MENU_GROUPS + " WHERE GRP_Group ='" + strGroup + "';";
                    SQLCmd.CommandType = CommandType.Text;

                    intNum = (int)SQLCmd.ExecuteScalar();

                    SQLConn.Close();
                    return intNum;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return intNum;
            }
        }

        public static List<string> GetMenuItemsForArea(string strArea)
        {
            /*
             Created 07/07/2025 By Roger Williams

             returns list of records in Menu_MenuItems that have passed area
          
             VARS

             strArea       - area


            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            bool blnOk = false;
            int intNum = 0;
            List<string> lstMenuItems = new List<string>();
            SqlDataReader sqlRead = null;


            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT * FROM " + CNST_STR_MENU_MENUITEMS + " WHERE MNU_DisplayWhere ='" + strArea + "' ORDER BY MNU_MenuItemName;";
                    SQLCmd.CommandType = CommandType.Text;

                    sqlRead = SQLCmd.ExecuteReader();

                    while (sqlRead != null)
                    {
                        lstMenuItems.Add(sqlRead.ToString());
                    }

                    sqlRead.Close();
                    SQLConn.Close();
                    return lstMenuItems;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return lstMenuItems;
            }
        }


        public static int GetLocationQty(string strLocation)
        {
            /*
                 Created 07/03/2025 By Roger Williams

                 Returns qty for passed location

            */
            {
                SqlConnection SQLConnStock;
                SqlCommand SQLCmdStock;
                SqlDataAdapter DADStock;
                DataSet DSTStock;


                try
                {
                    using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                    {
                        SQLConnStock.Open();
                        //load stock items
                        SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_LOC + " WHERE LOC_Location ='" + strLocation + "';", SQLConnStock);
                        DADStock = new SqlDataAdapter(SQLCmdStock);
                        DSTStock = new DataSet();
                        DADStock.Fill(DSTStock, CNST_STR_STOCK_LOC);

                        //check stock item record exist
                        if (DSTStock.Tables[CNST_STR_STOCK_LOC].Rows.Count == 0)
                        {
                            {
                                MessageBox.Show("Cannot Edit locations As No Stock Items Created!", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }

                return Convert.ToInt16(DSTStock.Tables[CNST_STR_STOCK_LOC].Rows[0]["LOC_Qty"]);
            }
        }



        public static void PopulateComboBoxes(ComboBox CMBTemp, string strTable, string strKeyField, string strKeyFieldValue, string strSecondFieldName, string strSecondFieldValue, string strWHERE, bool blnDistinct)
        {
            /*
              Created 17/02/2025 By Roger Williams

              Populates the comboboxes with table values using first non
              identity seed as column, unless user specifies a key field
              and optional sort value

              VARS

              CMBTemp             - combobox to populate
              strTable            - table to read from
          
              Optional:
             
              strKeyField         - key field name 
              strKeyFieldValue    - key field value always handled as text 
                                    in a commercial system would also pass
                                    data type
              strSecondFieldName  - another field name 
              strSecondFieldValue - another field value always handled as text 
                                    in a commercial system would also pass
                                    data type
             strWHERE             - specify WHERE clause
             blnDistinct          - use DISTINCT 
            
             Note: if using blnDistinct strKeyField MUST have a value!


            */

            SqlConnection SQLConn; 
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;

            //clear combo
            CMBTemp.Items.Clear();

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                   
                    if (blnDistinct)
                    { 
                        SQLCmd.CommandText = "SELECT DISTINCT " + strKeyField + " FROM " + strTable;
                    }
                    else
                    {
                        SQLCmd.CommandText = "SELECT * FROM " + strTable;
                    }
                    if (strKeyField.Length != 0 && blnDistinct == false)
                    {
                        SQLCmd.CommandText += " WHERE " + strKeyField + " = '" + strKeyFieldValue + "'";
                    }

                    if (strSecondFieldName.Length != 0)
                    {
                        SQLCmd.CommandText += " AND " + strSecondFieldName + " = '" + strSecondFieldValue + "'";
                    }

                    if (strWHERE.Length != 0)
                    {
                        if (strKeyField.Length != 0)
                        {
                            strWHERE = strWHERE.ToUpper();
                            strWHERE = strWHERE.Replace("WHERE", " AND ");
                            SQLCmd.CommandText += " " + strWHERE;
                        }
                        else
                        {
                            SQLCmd.CommandText += " " + strWHERE;
                        }
                    }
                    //add ;
                    SQLCmd.CommandText += ";";

                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    while (SQLRead.Read())
                    {
                        if (strTable == CNST_STR_LOCTRN)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == CNST_STR_LOTTRN)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == CNST_STR_STOCK_ITEMS)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == CNST_STR_STOCK_LOT)
                        {
                            CMBTemp.Items.Add(SQLRead[0].ToString());
                        }
                        if (strTable == CNST_STR_STOCK_VENDORS)
                        {
                            CMBTemp.Items.Add(SQLRead[2].ToString());
                        }
                        if (strTable == CNST_STR_LOGIN)
                        {
                           CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == CNST_STR_STOCK_LOC)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == CNST_STR_STOCK_PRODUCTFAMILY)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == CNST_STR_STOCK_UOM)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == CNST_STR_MENU_GROUPS)
                        {
                            CMBTemp.Items.Add(SQLRead[0].ToString());
                        }
                        if (strTable == CNST_STR_MENU_MENUITEMS)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == CNST_STR_MENU_AREAS)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == CNST_STR_MENU_USERSGROUPS)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                    }
                    
                    SQLRead.Close();
                    SQLConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Table " + strTable +  " - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public static bool ValidateLocationQtys(string strItemID, string strLoc)
        {
            /*
              Created 02/03/2025 By Roger Williams

              Compares location qty with total lot qtys (if any)

              Returns true if ok

              VARS

              stritemID     - item id to check
              strloc        - location to check

            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            DataSet DSTLOC;
            DataSet DSTLOT;
            SqlDataAdapter DADTemp;

            int intQtyLoc = 0;
            int intQtyLot = 0;
            DataTable tblTempLoc;
            DataTable tblTempLot;
            DataRow[] aryLots;

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    //get Location data
                    DSTLOC = new DataSet();
                    SQLCmd = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_LOC + " WHERE LOC_ItemID = '" + strItemID + "' AND LOC_Location = '" + strLoc + "';", SQLConn);
                    DADTemp = new SqlDataAdapter(SQLCmd);
                    DADTemp.Fill(DSTLOC, CNST_STR_STOCK_LOC);
                    //get lot data
                    DSTLOT = new DataSet();
                    SQLCmd = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_LOT + " WHERE LOT_ItemID = '" + strItemID + "' AND LOT_Location = '" + strLoc + "';", SQLConn);
                    DADTemp = new SqlDataAdapter(SQLCmd);
                    DADTemp.Fill(DSTLOT, CNST_STR_STOCK_LOT);

                    tblTempLoc = DSTLOC.Tables[CNST_STR_STOCK_LOC];

                    //iterate through the changed rows collating location and quantities
                    foreach (DataRow DARTemp in tblTempLoc.Rows)
                    {
                        //only one location record per item ID
                        intQtyLoc = Convert.ToInt16(DARTemp["LOC_Qty"]);
                        //reset lot qty var
                        intQtyLot = 0;

                        //check lots (if any)
                        tblTempLot = DSTLOT.Tables[CNST_STR_STOCK_LOT];

                        if (tblTempLot is null)
                        {
                            //no records hence no errors!
                            return true;
                        }
                        else
                        {
                            // aryLots = DSTLOT.Tables[CNST_STR_STOCK_LOT].Select("LOT_Location = '" + DARTemp["LOC_Location"] + "' AND LOT_ItemID ='" + strItemID + "'");

                            //  if (aryLots.Length == 0)
                            if (DSTLOT.Tables[CNST_STR_STOCK_LOT].Rows.Count == 0)
                            {
                                //no lots found no error!
                                return true;
                            }
                            else
                            {
                                //get lot qtys
                                foreach (DataRow DARLot in DSTLOT.Tables[CNST_STR_STOCK_LOT].Rows) // aryLots)
                                {
                                    intQtyLot += Convert.ToInt16(DARLot["LOT_Qty"]);
                                }

                                if (intQtyLot != intQtyLoc)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public static bool ProdFamUsed(string strProdFam)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns true if ANY stock item records exist with passed product family

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_ITEMS + " WHERE STKI_ProductFamily = '" + strProdFam + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, CNST_STR_STOCK_ITEMS);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
 
        public static bool UOMUsed(string strUOM)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns true if ANY stock item records exist with passed UOM

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + CNST_STR_STOCK_ITEMS + " WHERE STKI_UOM = '" + strUOM + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, CNST_STR_STOCK_ITEMS);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //class end
    }
}
