-- GoodFood Database Setup Script
-- Run this script as a PostgreSQL superuser (usually 'postgres')

-- Create the main application database
CREATE DATABASE goodfood_db_pub
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'en_US.utf8'
    LC_CTYPE = 'en_US.utf8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

-- Grant privileges to postgres user
GRANT ALL PRIVILEGES ON DATABASE goodfood_db_pub TO postgres;

-- Connect to the database to set up additional configurations
\c goodfood_db_pub;

-- Create any additional schemas or configurations if needed
-- (Currently using default 'public' schema)

-- Display confirmation
SELECT 'Database goodfood_db_pub created successfully!' AS status;
