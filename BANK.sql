DROP DATABASE IF EXISTS `bank`;
CREATE DATABASE `bank`;
USE `bank`;

CREATE TABLE IF NOT EXISTS customers (
 id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
 card_number VARCHAR(64) NOT NULL,
 pin_number INT NOT NULL,
 first_name VARCHAR(64) NOT NULL,
 last_name VARCHAR(64) NOT NULL,
 balance FLOAT NOT NULL
) ENGINE=INNODB;

INSERT INTO customers (card_number, pin_number, first_name, last_name, balance) VALUES
('123456789', 1234, 'Jaroslav', 'Kotrba', 1200.5),
('987654321', 4321, 'Anna', 'Ondrackova', 2200.5);