/*Create a Database named HELLSPRINTDATA*/
CREATE DATABASE HELLSPRINTDATA;

/*Create a table if not exists to prevent errors*/
CREATE TABLE IF NOT EXISTS LEADERBOARDS (
	username VARCHAR(20), 
	usertime FLOAT
);

/*Insert into the leaderboard with the current 
session username and score, these query have been modified
to suit the program and its function needs, _name and _time
is a parameter passed by the game itself*/
INSERT INTO LEADERBOARDS (username, usertime) 
VALUES ('" + _name + "' , '" + _time + "');

/*Refresh the table Leaderboard to only select the first 10
users that has the highest scores in a descending order, 
this query is used in deleting the excess user scores from
the table*/
DELETE FROM LEADERBOARDS 
WHERE usertime NOT IN (
	SELECT usertime FROM LEADERBOARDS 
	ORDER BY usertime 
	DESC LIMIT 10
);

/*Simply put, this will query will just collect all the datas inside
the table Leaderboard, this query is executed after refreshing the table*/
SELECT * FROM LEADERBOARDS 
ORDER BY usertime DESC;

/*<Use with Caution> Delete all the datas inside the table Leaderboard*/
DELETE FROM LEADERBOARDS;