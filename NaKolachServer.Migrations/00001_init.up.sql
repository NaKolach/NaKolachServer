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

    CONSTRAINT fk_user_id FOREIGN KEY(user_id) REFERENCES users(id) ON DELETE CASCADE
);

CREATE TABLE routes (
    id UUID PRIMARY KEY NOT NULL,
    author_id UUID,
    distance FLOAT8 NOT NULL,
    time BIGINT NOT NULL,
    path JSONB NOT NULL,
    categories VARCHAR(100)[] NOT NULL,
    points JSONB NOT NULL,
    created_at TIMESTAMPTZ NOT NULL,

    CONSTRAINT fk_author_id FOREIGN KEY(author_id) REFERENCES users(id) ON DELETE CASCADE
);

CREATE TABLE route_users (
    route_id UUID NOT NULL,
    user_id UUID NOT NULL,

    CONSTRAINT pk_route_users PRIMARY KEY (route_id, user_id),

    CONSTRAINT fk_route_users_route FOREIGN KEY (route_id) REFERENCES routes(id) ON DELETE CASCADE,
    CONSTRAINT fk_route_users_user FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);