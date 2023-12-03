/*Create a Database named HELLSPRINTDATA*/
CREATE DATABASE HELLSPRINTDATA;

/*Create a table if not exists to prevent errors*/
CREATE TABLE IF NOT EXISTS LEADERBOARDS (
	username VARCHAR(20), 
	usertime FLOAT
);