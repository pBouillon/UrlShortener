/*
 * Author
 *      Pierre Bouillon - https://github.com/pBouillon
 *
 * Repository
 *      UrlShortener - https://github.com/pBouillon/UrlShortener
 *
 * License
 *      MIT - https://github.com/pBouillon/UrlShortener/blob/master/LICENSE
 */

-- DROP TABLE IF EXISTS urlshortener;

CREATE TABLE IF NOT EXISTS urlshortener (
	id			SERIAL,
	raw			VARCHAR,
	shortened	VARCHAR		NOT NULL
);
