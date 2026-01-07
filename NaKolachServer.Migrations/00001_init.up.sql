DROP TABLE IF EXISTS users;

CREATE TABLE Users (
    id UUID NOT NULL,
    login TEXT NULL,
    email TEXT NULL,
    password TEXT NULL,
    CONSTRAINT pk_users PRIMARY KEY ("id")
);