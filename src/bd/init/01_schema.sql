CREATE TABLE geo_pr (
    pr_codigo        INTEGER PRIMARY KEY,
    pr_descripcion   VARCHAR(100) NOT NULL
);
CREATE TABLE geo_mn (
    mn_codigo        INTEGER PRIMARY KEY,
    mn_provincia     INTEGER NOT NULL,
    mn_descripcion   VARCHAR(150) NOT NULL,

    CONSTRAINT fk_mn_pr
        FOREIGN KEY (mn_provincia)
        REFERENCES geo_pr(pr_codigo)
);
CREATE TABLE geo_dp (
    cp_codigo_postal INTEGER NOT NULL,
    cp_codigo        INTEGER NOT NULL,
    cp_resto         INTEGER NOT NULL,
    cp_descripcion   VARCHAR(150),
    cp_municipio     INTEGER NOT NULL,

    CONSTRAINT pk_geo_cp
        PRIMARY KEY (cp_codigo_postal, cp_codigo),

    CONSTRAINT fk_cp_mn
        FOREIGN KEY (cp_municipio)
        REFERENCES geo_mn(mn_codigo)
);
CREATE TABLE alm_mar (
    mar_codigo        INTEGER PRIMARY KEY,
    mar_descripcion   VARCHAR(150) NOT NULL,
    mar_precio_1    NUMERIC(10,2) NULL,
    mar_precio_2    NUMERIC(10,2) NULL,
    mar_precio_3    NUMERIC(10,2) NULL,
    mar_precio_4    NUMERIC(10,2) NULL,
    mar_precio_5    NUMERIC(10,2) NULL
);
