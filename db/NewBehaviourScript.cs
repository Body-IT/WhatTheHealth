using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Data;
using System;


public class NewBehaviourScript : MonoBehaviour {
    public Text[] text1 = new Text[3];

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
            string stm = "SELECT * FROM members where " + parameterr.aa;
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            rdr = cmd.ExecuteReader();
            int k = 0;

            while (rdr.Read())
            {
                data itm = new data();
                itm.id = rdr["id"].ToString();
                itm.pwd = int.Parse(rdr["pwd"].ToString());
                itm.name1 = rdr["name1"].ToString();
                parameter.ss[k++] = itm.id + ' ' + itm.pwd.ToString() + ' ' + itm.name1;
                //Console.WriteLine(str);
            }
            parameter.cnt = 0;
            for(int i=k-1; i >=0; i--)
            {
                if (parameter.cnt == 20) break;
                parameterr.sss[parameter.cnt++] = parameter.ss[i];
                //Console.WriteLine(ss[i]);
                Debug.Log(parameter.ss[i]);
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

        for (int i = 0; i < 20; i++)
        {
            text1[i].text = " ";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < parameter.cnt; i++)
        {
            text1[i].text = parameterr.sss[i];
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Insert();
        }
    }
}
