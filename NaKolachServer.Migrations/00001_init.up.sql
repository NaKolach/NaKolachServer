DROP TABLE IF EXISTS users;

CREATE TABLE users (
    id UUID PRIMARY KEY NOT NULL,
    login TEXT NOT NULL,
    email TEXT NOT NULL,
    password_hash TEXT NOT NULL
);

CREATE TABLE user_refresh_tokens (
    id UUID PRIMARY KEY NOT NULL,
    user_id UUID NOT NULL,
    token VARCHAR(44) NOT NULL,
    is_revoked BOOLEAN NOT NULL,
    expires_at TIMESTAMPTZ NOT NULL,

    CONSTRAINT fk_user_id FOREIGN KEY(user_id) REFERENCES users(id) 
);

CREATE TABLE routes (
    id UUID PRIMARY KEY NOT NULL,
    author_id UUID,
    path JSONB NOT NULL,
    points JSONB NOT NULL,
    created_at TIMESTAMPTZ NOT NULL,

    CONSTRAINT fk_author_id FOREIGN KEY(author_id) REFERENCES users(id) 
);

CREATE TABLE route_users (
    route_id UUID NOT NULL,
    user_id UUID NOT NULL
);