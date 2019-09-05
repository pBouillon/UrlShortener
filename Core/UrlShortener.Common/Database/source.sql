-- DROP TABLE IF EXISTS urlshortener;

CREATE TABLE IF NOT EXISTS urlshortener (
	id			SERIAL,
	raw			VARCHAR,
	shortened	VARCHAR		NOT NULL
);
