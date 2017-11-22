using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class YgoPro : MonoBehaviour {

    private List<int> ygoNumbers = new List<int>();
    private List<string> ygoNames = new List<string>();

    // Use this for initialization
    void Start () {
        string conn = "URI=file:Assets/Scenes/Catchphrase/cards.cdb"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT id,name FROM texts";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            ygoNumbers.Add(reader.GetInt32(0));
            ygoNames.Add(reader.GetString(1));

                  
        }
        Debug.Log("value=" + ygoNumbers[5] + "  name =" + ygoNames[5]);
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
