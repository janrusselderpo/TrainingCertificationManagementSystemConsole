using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCMs
{
    class Program
    {

        static string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=TCMs;Integrated Security=True;MultipleActiveResultSets = True";
        static SqlConnection con;
        static SqlCommand cmd;
        
        static void Main(string[] args)
        {

            login();
        }
 

        static void MainMenu()
        {
            Console.WriteLine("Training Certification Management System");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("1 - Manage Employees " +
                              "\n2 - Manage Certificates" +
                              "\n0 - Log Out");

            Console.Write("Type a number:");
            string option = Console.ReadLine();
            Console.Clear();
            if (option == "1")
            {
                Employees();
            }
            if (option == "2")
            {
                Certificates();
            }
            if (option == "0")
            {
                login();
            }
            else
            {
                Console.WriteLine("Invalid option please select again\n\n");
                MainMenu();
            }
        }

        static void Employees()
        {
            Console.Clear();
            Console.WriteLine("MANAGE EMPLOYEES");
            Console.WriteLine("----------------");
            Console.WriteLine("1 - Add employee to database");
            Console.WriteLine("2 - Edit employee information in database");
            Console.WriteLine("3 - Remove an Employee from the database");
            Console.WriteLine("4 = View certificates by employee");
            Console.WriteLine("0 = Return to main menu");
            Console.WriteLine("Select an option:");
            string option = Console.ReadLine();
            Console.Clear();
            if (option == "1")
            {
                add();
            }
            if (option == "2")
            {
                edit();
            }
            if (option == "3")
            {
                delete();
            }
            if (option == "4")
            {
                //SHOW SQL TABLE OF EMPLOYEES CERTIFICATE
                certified();
            }
            if (option == "0")
            {
                MainMenu();
            }
            else
            {
                Console.WriteLine("Invalid option please select again\n\n");
                Employees();
            }
        }

        static void Certificates()
        {
            Console.Clear();
            Console.WriteLine("CERTIFICATES MANAGER");
            Console.WriteLine("----------------");
            Console.WriteLine("1 - Manage Certificates");
            Console.WriteLine("2 - Manage Requests");
            Console.WriteLine("0 = Return to main menu");
            Console.WriteLine("Select an option: ");
            string option = Console.ReadLine();
            Console.Clear();

            if (option == "1")
            {
                ManageCertificates();
            }
            if (option == "2")
            {
                ManageRequests();
            }
            if (option == "0")
            {
                MainMenu();
            }
            else
            {
                Console.WriteLine("Invalid option please select again\n\n");
                Certificates();
            }
        }

        static void add()
        {
            Console.Clear();
            Console.WriteLine("Add an employee to data base?");
            Console.WriteLine("1 - continue| 0 - return to employee menu");

            String Choice = Console.ReadLine();
            if(Choice == "1")
            { 
                Console.WriteLine("Enter First name:");
                String fName = Console.ReadLine();
                Console.WriteLine("Enter Last Name");
                String lName = Console.ReadLine();
                Console.WriteLine("Enter Position:");
                String occP = Console.ReadLine();
                Console.WriteLine("Enter Email:");
                String email = Console.ReadLine();

                con = new SqlConnection(conn);
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into employees(FirstName, LastName, Position, Email) values ('" + fName + "','" + lName + "','" + occP + "','" + email + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                Console.WriteLine("Employee has been added to database");
                Console.WriteLine("Press any key to add again");
                add();
            }
            else
            {
                Employees();
            }

        }

        static void edit()
        {
            Console.Clear();
            // Show employee table
            con = new SqlConnection(conn);
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM employees", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                con.Close();
            }

            Console.WriteLine("Edit employee information in database");
            Console.WriteLine("1 - continue | 0 - return to employee menu");
            String option = Console.ReadLine();
            
            if (option == "1")
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("id, First Name, Last Name, Postion, Email");
                Console.WriteLine(DumpDataTable(dt));
                Console.WriteLine("-----------------------------------");

                Console.WriteLine("\nSelect an employee Id from the table above to edit:");
                String optID = Console.ReadLine();

                Console.WriteLine("\nChoose an option to continue");
                Console.WriteLine("1 - edit by column | 2 - Edit all columns");
                String select = Console.ReadLine();
                if(select == "1")
                {
                    Console.WriteLine("Select column to edit");
                    Console.WriteLine("1 - First Name | 2 - Last Name | 3 - Position |4 - Email");
                
                    String choice = Console.ReadLine();
                    if(choice == "1")
                    {
                        Console.WriteLine("Enter First Name");
                        String editable = Console.ReadLine().ToLower();
                        con = new SqlConnection(conn);
                        con.Open();
                    
                        cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE employees Set FirstName = '"+editable+"'  Where id = '"+optID+"'";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine("\nfirst name has been updated");
                        Console.WriteLine("Enter any key to edit again");
                        String restart = Console.ReadLine();
                    
                        edit();
                    }
                    if (choice == "2")
                    {
                        Console.WriteLine("Enter Last Name");
                        String editable = Console.ReadLine().ToLower();
                        con = new SqlConnection(conn);
                        con.Open();

                        cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE employees Set LastName = '" + editable + "'  Where id = '" + optID + "'";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine("\nLast name has been updated");
                        Console.WriteLine("Enter any key to edit again");
                        String restart = Console.ReadLine();

                        edit();
                    }
                    if (choice == "3")
                    {
                        Console.WriteLine("Enter Position");
                        String editable = Console.ReadLine().ToLower();
                        con = new SqlConnection(conn);
                        con.Open();

                        cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE employees Set Postion = '" + editable + "'  Where id = '" + optID + "'";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine("\nPosition has been updated");
                        Console.WriteLine("Enter any key to edit again");
                        String restart = Console.ReadLine();

                        edit();
                    }
                    if (choice == "4")
                    {
                        Console.WriteLine("Enter Email");
                        String editable = Console.ReadLine().ToLower();
                        con = new SqlConnection(conn);
                        con.Open();

                        cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE employees Set Email = '" + editable + "'  Where id = '" + optID + "'";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine("\nEmail has been updated");
                        Console.WriteLine("Enter any key to edit again");
                        String restart = Console.ReadLine();

                        edit();
                    }
                    else
                    {
                        Console.WriteLine("\nPlease select an input given from above");
                        Console.WriteLine("Restarting edit");
                        edit();
                    }
                } 
                else
                {
                    Console.WriteLine("Enter First Name");
                    String fName = Console.ReadLine().ToLower();
                    Console.WriteLine("Enter Last Name");
                    String lName = Console.ReadLine().ToLower();
                    Console.WriteLine("Enter Position");
                    String pos = Console.ReadLine().ToLower();
                    Console.WriteLine("Enter Email");
                    String email = Console.ReadLine().ToLower();

                    con = new SqlConnection(conn);
                    con.Open();

                    cmd = new SqlCommand("UPDATE employees Set FirstName = @F, Lastname = @L , Position = @P , Email = @E Where id = @I", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@I", optID); 
                    cmd.Parameters.AddWithValue("@F", fName);
                    cmd.Parameters.AddWithValue("@L", lName);
                    cmd.Parameters.AddWithValue("@P", pos);
                    cmd.Parameters.AddWithValue("@E", email);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Console.WriteLine("\nEmployee has been updated");
                    Console.WriteLine("Enter any key to edit again");
                    String restart = Console.ReadLine();

                    edit();

                }
                
            } 
            else
            {
                Employees();
            }

        }

        static void delete()
        {
            Console.Clear();
            // Show employee table
            
            con = new SqlConnection(conn);
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM employees", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                con.Close();
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("id, First Name, Last Name, Postion, Email");
            Console.WriteLine(DumpDataTable(dt));
            Console.WriteLine("-----------------------------------");

            Console.WriteLine("Delete employee information in database");
            Console.WriteLine("1 - continue | 0 - return to employee menu");
            String option = Console.ReadLine();

            if (option == "1")
            {
                Console.WriteLine("Select an employee ID to delete");
                String optID = Console.ReadLine();       
                Console.WriteLine("Delete Employee? \n1 - Continue | 2 - Return");
                String del = Console.ReadLine();

                if (del == "1")
                {
                    con.Open();
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM employees where id = '" + optID + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                Console.WriteLine("Employee has been deleted to database");
                Console.WriteLine("Press any key to delete again");
                Console.Clear();
                delete();
                }
                else 
                {
                    delete();
                }
            }
            else 
            {
                Employees();
            }
        }

        static void ManageCertificates()
        {
            Console.Clear();
            Console.WriteLine("MANAGE CERTIFICATES");
            Console.WriteLine("----------------");
            Console.WriteLine("1 - Add certifiacte to database");
            Console.WriteLine("2 - Edit certificate in database");
            Console.WriteLine("3 - Remove a certifcate from the database");
            Console.WriteLine("0 = Return to main menu");
            Console.WriteLine("Select an option:");
            string option = Console.ReadLine();
            Console.Clear();
            if (option == "1")
            {
                addC();
            }
            if (option == "2")
            {
                editC();
            }
            if (option == "3")
            {
                deleteC();
            }
            if (option == "0")
            {
                Certificates();
            }
            else
            {
                Console.WriteLine("Invalid option please select again\n\n");
                ManageCertificates();
            }
        }
        static void addC()
        {
            Console.Clear();
            Console.WriteLine("Add a certificate to data base?");
            Console.WriteLine("1 - continue| 0 - return to Manage Certificates menu");

            String Choice = Console.ReadLine();
            if (Choice == "1")
            {
                Console.WriteLine("Enter Certificate name:");
                String cert = Console.ReadLine();
                Console.WriteLine("Enter Code");
                String code = Console.ReadLine();
                Console.WriteLine("Enter Validty(by months):");
                String valD = Console.ReadLine();


                con = new SqlConnection(conn);
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into certificates(Certificate, Code, Validity) values ('" + cert + "','" + code + "','" + valD + "')";
                cmd.ExecuteNonQuery();
                con.Close();

                Console.WriteLine("Employee has been added to database");
                Console.WriteLine("Press any key to add again");
                String option = Console.ReadLine();
                addC();
            }
            else
            {
                ManageCertificates();
            }
        }
        static void editC()
        {
            Console.Clear();
            // Show employee table
            con = new SqlConnection(conn);
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM certificates", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                con.Close();
            }

            Console.WriteLine("Edit certificate information in database");
            Console.WriteLine("1 - continue | 0 - return to Manage Certificates menu");
            String option = Console.ReadLine();

            if (option == "1")
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("id, Certificate, Code, Validity");
                Console.WriteLine(DumpDataTable(dt));
                Console.WriteLine("-----------------------------------");

                Console.WriteLine("\nSelect a certifcate Id from the table above to edit:");
                String optID = Console.ReadLine();

                Console.WriteLine("\nChoose an option to continue");
                Console.WriteLine("1 - edit by column | 2 - Edit all columns");
                String select = Console.ReadLine();
                if (select == "1")
                {
                    Console.WriteLine("Select column to edit");
                    Console.WriteLine("1 - Certificate name | 2 - Code | 3 - Validity ");

                    String choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        Console.WriteLine("Enter Certificate Name");
                        String editable = Console.ReadLine().ToLower();
                        con = new SqlConnection(conn);
                        con.Open();

                        cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE certificates Set Certificate = '" + editable + "'  Where id = '" + optID + "'";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine("\nCertificate Name has been updated");
                        Console.WriteLine("Enter any key to edit again");
                        String restart = Console.ReadLine();

                        editC();
                    }
                    if (choice == "2")
                    {
                        Console.WriteLine("Enter Code");
                        String editable = Console.ReadLine().ToUpper();
                        con = new SqlConnection(conn);
                        con.Open();

                        cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE certificates Set Code = '" + editable + "'  Where id = '" + optID + "'";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine("\nCode has been updated");
                        Console.WriteLine("Enter any key to edit again");
                        String restart = Console.ReadLine();

                        editC();
                    }

                    if (choice == "3")
                    {
                        Console.WriteLine("Enter Validity(By Months)");
                        String editable = Console.ReadLine();
                        con = new SqlConnection(conn);
                        con.Open();

                        cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE certificates Set Validity = '" + editable + "'  Where id = '" + optID + "'";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine("\nValidity has been updated");
                        Console.WriteLine("Enter any key to edit again");
                        String restart = Console.ReadLine();

                        edit();
                    }
                    else
                    {
                        Console.WriteLine("\nPlease select an input given from above");
                        Console.WriteLine("Restarting edit");
                        editC();
                    }
                }
                else
                {
                    Console.WriteLine("Enter Certificate name");
                    String cert = Console.ReadLine().ToLower();
                    Console.WriteLine("Enter Code");
                    String code = Console.ReadLine().ToUpper();
                    Console.WriteLine("Enter Validity");
                    String valD = Console.ReadLine().ToLower();

                    con = new SqlConnection(conn);
                    con.Open();

                    cmd = new SqlCommand("UPDATE certificates Set Certificate = @C, Code = @CD , Validity = @V , Email = @E Where id = @I", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@I", optID);
                    cmd.Parameters.AddWithValue("@C", cert);
                    cmd.Parameters.AddWithValue("@CD", code);
                    cmd.Parameters.AddWithValue("@V", valD);


                    cmd.ExecuteNonQuery();
                    con.Close();
                    Console.WriteLine("\nCertificate has been updated");
                    Console.WriteLine("Enter any key to edit again");
                    String restart = Console.ReadLine();

                    editC();

                }

            }
            else
            {
                ManageCertificates();
            }
        }
        static void deleteC()
        {
            Console.Clear();
            // Show employee table

            con = new SqlConnection(conn);
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM certificates", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                con.Close();
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("id, Certificate, Code, Validity");
            Console.WriteLine(DumpDataTable(dt));
            Console.WriteLine("-----------------------------------");

            Console.WriteLine("Delete certificate in database");
            Console.WriteLine("1 - continue | 0 - return to Manage Certificates menu");
            String option = Console.ReadLine();

            if (option == "1")
            {
                Console.WriteLine("Select a certificate ID to delete");
                String optID = Console.ReadLine();
                Console.WriteLine("Delete certificate? \n1 - Continue | 2 - Return");
                String del = Console.ReadLine();

                if (del == "1")
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM certificates where id = '" + optID + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Console.WriteLine("Certificate has been deleted to database");
                    Console.WriteLine("Press any key to delete again");
                    Console.Clear();
                    deleteC();
                }
                else
                {
                    deleteC();
                }
            }
            else
            {
                ManageCertificates();
            }
        }

        static void ManageRequests()
        {
            Console.Clear();
            Console.WriteLine("MANAGE CERTIFICATES");
            Console.WriteLine("----------------");
            Console.WriteLine("1 - Accept Requests");
            Console.WriteLine("2 - Delete Requests");
            Console.WriteLine("0 = Return to main menu");
            Console.WriteLine("Select an option:");
            string option = Console.ReadLine();
            Console.Clear();
            if (option == "1")
            {
                aReq();
            }
            if (option == "2")
            {
                dReq();
            }
            if (option == "0")
            {
                MainMenu();
            }
            else
            {
                Console.WriteLine("Invalid option please select again\n\n");
                Certificates();
            }
        }

        static void aReq()
        {
            Console.Clear();
            Console.WriteLine("Accept Requests?");
            Console.WriteLine("1 - yes | 0 - return to Manage Certificates");
            String Option = Console.ReadLine();

            if (Option == "1")
            {
                Console.Clear();
                con = new SqlConnection(conn);
                DataTable dt = new DataTable();
                Console.Clear();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ForCertification", con))
                {
                    SqlDataAdapter tb = new SqlDataAdapter(cmd);
                    con.Open();
                    tb.Fill(dt);
                    con.Close();
                }
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("id, First Name, Last Name, CertificateCode, DateRequested");
                Console.WriteLine(DumpDataTable(dt));
                Console.WriteLine("-----------------------------------");

                Console.WriteLine("Select values from the table above only");
                Console.WriteLine("Enter request id to accept");
                string optID = Console.ReadLine();

                con.Open();
                DataTable dt1 = new DataTable();
                cmd = new SqlCommand("SELECT id, FirstName, LastName, CertificateCode FROM ForCertification Where id ='" + optID + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                String fname = dt1.Rows[0].ItemArray[1].ToString();
                String lname = dt1.Rows[0].ItemArray[2].ToString();
                String ccode = dt1.Rows[0].ItemArray[3].ToString();
                if (dt1.Rows.Count >= 1)
                {
                    cmd = new SqlCommand("SELECT id, Validity FROM certificates Where code ='" + ccode + "' ", con);
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count >= 1)
                    {

                        String code = dt2.Rows[0].ItemArray[1].ToString();
                        var date = DateTime.Now.Date;
                        var expiry = date.AddMonths(Convert.ToInt32(code));
                        using(con = new SqlConnection(conn))
                        {
                               
                                SqlCommand cmd = new SqlCommand("insert into CertifiedEmployees(FirstName, LastName, CertificateCode, DateIssued, ExpiryDate) values('" + fname + "', '" + lname + "', '" + ccode + "', '" + date + "', '" + expiry + "')",con);
                                cmd.Connection.Open();
                                cmd.ExecuteNonQuery();   
                        }

                        con.Close();
                        
                        Console.WriteLine("Requests accepted, certificate has been issued");
                        Console.WriteLine("Press any key to continue");
                        String option = Console.ReadLine();
                            aReq();
                    }
                    else
                    {

                    }
                }
                else
                {
                    Console.WriteLine("First Name entered doesnt exists in the database");
                    userM();
                }
            }
            else
            {
                ManageRequests();
            }
        }
        static void dReq()
        {
            Console.Clear();
            // Show employee table

            con = new SqlConnection(conn);
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM ForCertification", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                con.Close();
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("id, Certificate, Code, Validity");
            Console.WriteLine(DumpDataTable(dt));
            Console.WriteLine("-----------------------------------");

            Console.WriteLine("Delete certificate in database");
            Console.WriteLine("1 - continue | 0 - return to Manage Certificates menu");
            String option = Console.ReadLine();

            if (option == "1")
            {
                Console.WriteLine("Select a Requests ID to delete");
                String optID = Console.ReadLine();
                Console.WriteLine("Delete Requests? \n1 - Continue | 2 - Return");
                String del = Console.ReadLine();

                if (del == "1")
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM ForCertification where id = '" + optID + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Console.WriteLine("Requests has been deleted to database");
                    Console.WriteLine("Press any key to delete again");
                    Console.Clear();
                    dReq();
                }
                else
                {
                    dReq();
                }
            }else
            {
                ManageRequests();
            }
        }


        static void login()
        {
            Console.Clear();
            Console.WriteLine("LOGIN");
            Console.WriteLine("Username");
            String user = Console.ReadLine().ToString().ToLower();
            Console.WriteLine("Password");
            String pass = Console.ReadLine().ToString().ToLower(); ;
            Console.Clear();
            if (user == "admin" || pass == "admin")
            {
                MainMenu();
            }
            if (user == "user" || pass == "")
            {
                userM();
            }
            if (user == "" || pass == "")
            {
                login();
            }
            if (user.Length >= 0 || pass.Length >= 0)
            {
                login();
            }
            else
            {
                Console.WriteLine("Incorrect username or password please try again");
                login();
            }
        }
        static void  userM()
        {
            Console.Clear();
            Console.WriteLine("EMPLOYEE MENU");
            Console.WriteLine("--------------");
            Console.WriteLine("1 - File a certificate request");
            Console.WriteLine("0 - Log out");
            String req = Console.ReadLine();
            if(req == "1")
            {
                FileRequests();
            }
            else
            {
                login();
            }

        }

        static void FileRequests()
        {

            con = new SqlConnection(conn);
            DataTable dt = new DataTable();
            Console.Clear();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM employees", con))
            {
                SqlDataAdapter tb = new SqlDataAdapter(cmd);
                con.Open();
                tb.Fill(dt);
                con.Close();
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("id, First Name, Last Name, Postion, Email");
            Console.WriteLine(DumpDataTable(dt));
            Console.WriteLine("-----------------------------------");


            Console.WriteLine("File a Requests?");
            Console.WriteLine("1 - Yes | 2 - no");
            String option = Console.ReadLine();
            if (option == "1")
            {
                Console.WriteLine("Select values from the table above only");
                Console.WriteLine("Enter Employee ID");
                string optID = Console.ReadLine();

                con.Open();
                DataTable dt1 = new DataTable(); ;
                cmd = new SqlCommand("SELECT id, FirstName, LastName FROM employees Where id = '" + optID + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                if (dt1.Rows.Count >= 1)
                {
                    String fname = dt1.Rows[0].ItemArray[1].ToString();
                    String lname = dt1.Rows[0].ItemArray[2].ToString();

                    DataTable dt2 = new DataTable();
                    using (SqlCommand cmd2 = new SqlCommand("SELECT * FROM certificates", con))
                    {

                        SqlDataAdapter dta = new SqlDataAdapter(cmd2);
                        dta.Fill(dt2);

                    }
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("id, Certificate, Code, Validity");
                    Console.WriteLine(DumpDataTable(dt2));
                    Console.WriteLine("-----------------------------------");

                    Console.WriteLine("Enter certificate id from table above to select certificate code ");
                    //display table
                    String cID = Console.ReadLine().ToUpper();


                    using (SqlCommand cmd3 = new SqlCommand("SELECT id, Code FROM certificates Where id ='" + cID + "'", con))
                    {
                        DataTable dt3 = new DataTable();
                        SqlDataAdapter ad2 = new SqlDataAdapter(cmd3);
                        ad2.Fill(dt3);
                        String code = dt3.Rows[0].ItemArray[1].ToString();
                        if (dt3.Rows.Count >= 1)
                        {
                            var date = DateTime.Now.Date;
                            cmd3.CommandType = CommandType.Text;
                            cmd3.CommandText = "insert into ForCertification(FirstName, LastName, CertificateCode, DateRequested) values ('" + fname + "','" + lname + "','" + code + "', '" + date + "')";
                            cmd3.ExecuteNonQuery();
                            con.Close();
                            FileRequests();
                        }
                        else
                        {
                            Console.WriteLine("Code does not exists");
                            FileRequests();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("First Name entered doesnt exists in the database");
                    FileRequests();
                }
            }
            else;
            {
                userM();
            }
        }

        static void certified()
        {

            Console.Clear();
            // Show employee table

            con = new SqlConnection(conn);
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM CertifiedEmployees", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                con.Close();
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("id, FirstName, LastName, CertCode, DateIssued, ExpiryDate");
            Console.WriteLine(DumpDataTable(dt));
            Console.WriteLine("-----------------------------------");

            Console.WriteLine("1 - Delete a row | 0 - return to Manage Employees");
            String select = Console.ReadLine();

            if (select == "1")
            {
                

                Console.WriteLine("Delete certificate in database");
                Console.WriteLine("1 - continue | 2 - return to Manage Certificates menu");
                String option = Console.ReadLine();

                if (option == "1")
                {
                    Console.WriteLine("Select an employee ID to delete");
                    String optID = Console.ReadLine();
                    Console.WriteLine("Delete employee certificate? \n1 - Continue | 2 - Return");
                    String del = Console.ReadLine();

                    if (del == "1")
                    {
                        con.Open();
                        cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "DELETE FROM CertifiedEmployees where id = '" + optID + "'";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine("employee certificateertificate has been deleted to database");
                        Console.WriteLine("Press any key to delete again");
                        Console.Clear();
                        certified();
                    }
                    else
                    {
                        certified();
                    }
                }
                else
                {
                    certified();
                }
            }
            else
            {
                Employees();
            }
                
        }

      
        //Table display
        public static string DumpDataTable(DataTable table)
        {
            string data = string.Empty;
            StringBuilder sb = new StringBuilder();
            
    
            if (null != table && null != table.Rows)
            {
                foreach (DataRow dataRow in table.Rows)
                {
                    foreach (var item in dataRow.ItemArray)
                    {
                        sb.Append(item);
                        sb.Append(',');
                    }
                    sb.AppendLine();
                }
                data = sb.ToString();
            }
            return data;
        }

    }
}
