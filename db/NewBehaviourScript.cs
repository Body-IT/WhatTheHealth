using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Data;
using System;


public class NewBehaviourScript : MonoBehaviour {

    public struct data
    {
        public int pwd;
        public string id, name1;
    }

    private static void SelectUsingAdapter()
    {
        DataSet ds = new DataSet();
        string connStr = "server=localhost;Uid=root; password=qwe123; database=db_test";
        MySqlConnection conn = null;
        MySqlDataReader rdr = null;
        try
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
            string stm = "SELECT * FROM members";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                data itm = new data();
                itm.id = rdr["id"].ToString();
                itm.pwd = int.Parse(rdr["pwd"].ToString());
                itm.name1 = rdr["name1"].ToString();
                string str = itm.id + ' ' + itm.pwd.ToString() + ' ' + itm.name1;
                //Console.WriteLine(str);
                Debug.Log(str);

            }
        }
        catch (Exception e)
        {

        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
        }

    }


    private static void Insert()
    {
        DataSet ds = new DataSet();
        string connStr = "server=localhost;Uid=root; password=qwe123; database=db_test";
        MySqlConnection conn = null;
        MySqlDataReader rdr = null;
        try
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
            string stm = "insert into members values('t',2,'q');";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine(rdr.GetString(1));
            }
        }
        catch (Exception e)
        {

        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
        }

    }

    // Use this for initialization
    void Start()
    {
        //Insert();
        SelectUsingAdapter();
        //Debug.Log("HI");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Insert();
        }
    }
}