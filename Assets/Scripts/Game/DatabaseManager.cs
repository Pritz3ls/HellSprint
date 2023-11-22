// Retrieve and Save huge chunks of save data and records
// Connection to local database, not remotely
// Using SQLite for this one, already derivative of MonoBehaviour
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
public class DatabaseManager : MonoBehaviour{
    private string dbNAME = "URI=file:Leaderboard.db";
    public static DatabaseManager database;
    public List<LeaderboardItem> LeadItem = new List<LeaderboardItem>();
    void Start(){
        if(database == null){
            database = this;
        }else{Destroy(gameObject);}
        CreateDB();

        // DeleteRecords();
    }
    // Create the database if not exist
    private void CreateDB(){
        // Setup the Database Connection
        using(var connection = new SqliteConnection(dbNAME)){
            connection.Open();
            // Setup a command control to allow Database controls
            using(var command = connection.CreateCommand()){
                command.CommandText = "CREATE TABLE IF NOT EXISTS LEADERBOARDS (username VARCHAR(20), usertime FLOAT);";
                command.ExecuteNonQuery();
            }
            // Close the connection right after
            connection.Close();
        }
    }
    // Add a new Leader from the current Session
    public void AddLeader(string _name, float _time){
        using(var connection = new SqliteConnection(dbNAME)){
            connection.Open();
            // Setup a command control to allow Database controls
            using(var command = connection.CreateCommand()){
                // Insert into the leaderboard with the current session username and score
                command.CommandText = "INSERT INTO LEADERBOARDS (username, usertime) VALUES ('" + _name + "' , '" + _time + "');";
                command.ExecuteNonQuery();
            }
            // Close the connection right after
            connection.Close();
        }
    }
    public void RefreshLeaders(){
        using(var connection = new SqliteConnection(dbNAME)){
            connection.Open();
            // Setup a command control to allow Database controls
            using(var command = connection.CreateCommand()){
                command.CommandText = "DELETE FROM LEADERBOARDS WHERE usertime NOT IN (SELECT usertime FROM LEADERBOARDS ORDER BY usertime DESC LIMIT 10);";
                command.ExecuteNonQuery();
            }
            // Close the connection right after
            connection.Close();
        }
    }
    public void CollectLeaders(){
        LeadItem.Clear();
        using(var connection = new SqliteConnection(dbNAME)){
            connection.Open();
            // Setup a command control to allow Database controls
            using(var command = connection.CreateCommand()){
                command.CommandText = "SELECT * FROM LEADERBOARDS ORDER BY usertime DESC;";
                using(IDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        // Lets display it on the console for now
                        // Debug.Log($"Username {reader["username"]}\tTime {reader["usertime"]}");
                        LeadItem.Add(new LeaderboardItem(reader["username"].ToString(), reader["usertime"].ToString()));
                    }
                }
            }
            // Close the connection right after
            connection.Close();
        }
    }

    // Use with Caution, this will delete all records
    private void DeleteRecords(){
        using(var connection = new SqliteConnection(dbNAME)){
            connection.Open();
            // Setup a command control to allow Database controls
            using(var command = connection.CreateCommand()){
                command.CommandText = "DELETE FROM LEADERBOARDS;";
                command.ExecuteNonQuery();
            }
            // Close the connection right after
            connection.Close();
        }
    }
}

public class LeaderboardItem{
    public string lead_username = "<empty>";
    public string lead_usertime = "0.000";

    public LeaderboardItem(string _name, string _time){
        lead_username = _name;
        lead_usertime = _time;
    }
}