INSERT INTO geo_pr (pr_codigo, pr_descripcion) VALUES
(28, 'Madrid'),
(38, 'Santa Cruz de Tenerife'),
(13, 'Ciudad Real');

INSERT INTO geo_mn (mn_codigo, mn_provincia, mn_descripcion) VALUES
(28079, 28, 'Madrid'),
(28013, 28, 'Getafe'),

(38038, 38, 'Santa Cruz de Tenerife'),
(38028, 38, 'La Laguna'),

(13034, 13, 'Ciudad Real'),
(13023, 13, 'Almadén');

-- Madrid
INSERT INTO geo_dp VALUES
(28001, 1, 1, 'Centro', 28079),
(28001, 2, 1,'Barrio Salamanca', 28079),
(28901, 901, 1,'Getafe Centro', 28013);

-- Tenerife
INSERT INTO geo_dp VALUES
(38001, 1, 1,'Centro SC', 38038),
(38201, 1, 201,'La Laguna Centro', 38028);

-- Ciudad Real (caso típico)
INSERT INTO geo_dp VALUES
(13001, 1, 1, 'Ciudad Real Centro', 13034);

-- Maestro de articulos
INSERT INTO alm_mar VALUES
(1, 'Coca Cola', 1.50, 1.40, 1.30, 1.20, 1.10),
(2, 'Pepsi', 1.45, 1.35, 1.25, 1.15, 1.05),
(3, 'Fanta', 1.40, 1.30, 1.20, 1.10, 1.00),
(4, 'Sprite', 1.35, 1.25, 1.15, 1.05, 0.95),
(5, '7 Up', 1.30, 1.20, 1.10, 1.00, 0.90),
(6, 'Mountain Dew', 1.25, 1.15, 1.05, 0.95, 0.85),
(7, 'Dr Pepper', 1.20, 1.10, 1.00, 0.90, 0.80),
(8, 'Red Bull', 2.00, 1.90, 1.80, 1.70, 1.60),
(9, 'Monster', 1.80, 1.70, 1.60, 1.50, 1.40),
(10,'Gatorade', 1.75, 1.65, 1.55, 1.45, 1.35),
(11,'Powerade', 1.70, 1.60, 1.50, 1.40, 1.30),
(12,'Aquarius', 1.65, 1.55, 1.45, 1.35, 1.25),
(13,'Lipton Ice Tea', 1.60, 1.50, 1.40, 1.30, 1.20),
(14,'Nestea', 1.55, 1.45, 1.35, 1.25, 1.15),
(15,'Arizona', 1.50, 1.40, 1.30, 1.20, 1.10),
(16,'Snapple', 1.45, 1.35, 1.25, 1.15, 1.05),
(17,'Fresca', 1.40, 1.30, 1.20, 1.10, 1.00),
(18,'Sunkist', 1.35, 1.25, 1.15, 1.05, 0.95),
(19,'Barqs', 1.30, 1.20, 1.10, 1.00, 0.90),
(20,'Mello Yello', 1.25, 1.15, 1.05, 0.95, 0.85);

