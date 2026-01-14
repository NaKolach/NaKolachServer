DROP TABLE IF EXISTS users;

CREATE TABLE Users (
    id UUID PRIMARY KEY NOT NULL,
    login TEXT NOT NULL,
    email TEXT NOT NULL,
    password_hash TEXT NOT NULL
);