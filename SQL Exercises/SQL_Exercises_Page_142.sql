-- Handling Dropping of Columns due to UNIQUE Constraint
BEGIN TRANSACTION;

ALTER TABLE workers RENAME TO workers_old;

CREATE TABLE IF NOT EXISTS workers (
    worker_id INTEGER PRIMARY KEY,
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    email TEXT NOT NULL,
    phone TEXT NOT NULL,
    salary INTEGER DEFAULT 1000
);

INSERT INTO workers (worker_id, first_name, last_name, email, phone, salary)
SELECT worker_id, first_name, last_name, email, phone, salary FROM workers_old;

DROP TABLE workers_old;

COMMIT;

PRAGMA foreign_keys = ON;


-- Renaming
BEGIN TRANSACTION;

ALTER TABLE workers RENAME TO factory_workers;

ALTER TABLE factory_workers RENAME COLUMN salary TO wage;

COMMIT;

-- Creating Triggers
CREATE TABLE IF NOT EXISTS factory_workers_history (
    worker_id INTEGER,
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    email TEXT NOT NULL,
    phone TEXT NOT NULL,
    wage INTEGER DEFAULT 1000,
    change_type TEXT,
    change_timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TRIGGER IF NOT EXISTS after_insert_factory_workers
AFTER INSERT ON factory_workers
BEGIN
    INSERT INTO factory_workers_history(worker_id, first_name, last_name, email, phone, wage, change_type)
    VALUES (NEW.worker_id, NEW.first_name, NEW.last_name, NEW.email, NEW.phone, NEW.wage, 'INSERT');
END;

CREATE TRIGGER IF NOT EXISTS after_delete_factory_workers
AFTER DELETE ON factory_workers
BEGIN
    INSERT INTO factory_workers_history(worker_id, first_name, last_name, email, phone, wage, change_type)
    VALUES (OLD.worker_id, OLD.first_name, OLD.last_name, OLD.email, OLD.phone, OLD.wage, 'DELETE');
END;

-- Using CASE to categorize workers based on their wage
SELECT
    first_name,
    last_name,
    wage,
    CASE
        WHEN wage < 60000 THEN 'Low Wage'
        WHEN wage >= 60000 AND wage < 80000 THEN 'Medium Wage'
        ELSE 'High Wage'
    END AS wage_category
FROM
    factory_workers;

-- Using UNION to combine two SELECT statements
SELECT
    first_name,
    last_name,
    wage
FROM
    factory_workers
WHERE
    wage >= 80000

UNION

SELECT
    first_name,
    last_name,
    wage
FROM
    factory_workers
WHERE
    wage < 50000;

-- Using INTERSECT to find workers with common characteristics
SELECT
    first_name,
    last_name,
    wage
FROM
    factory_workers
WHERE
    last_name GLOB 'D*'

INTERSECT

SELECT
    first_name,
    last_name,
    wage
FROM
    factory_workers
WHERE
    wage >= 60000;

-- Using EXCEPT to find workers in the first set but not in the second set
SELECT
    first_name,
    last_name,
    wage
FROM
    factory_workers
WHERE
    last_name GLOB 'S*'

EXCEPT

SELECT
    first_name,
    last_name,
    wage
FROM
    factory_workers
WHERE
    wage >= 70000;

-- Using GLOB to filter workers based on a pattern
SELECT
    first_name,
    last_name,
    wage
FROM
    factory_workers
WHERE
    first_name GLOB 'J*';

-- Creating an Index on first_name, last_name and wage Columns
CREATE INDEX idx_name_salary ON factory_workers (first_name, last_name, wage);



