CREATE TABLE membership.Users (
    user_idx INT PRIMARY KEY AUTO_INCREMENT,
    user_id VARCHAR(45) NOT NULL UNIQUE,
    user_password	varchar(200) NOT NULL,
    user_name	varchar(45)	NOT NULL,
    user_email	varchar(45)	NOT NULL
);