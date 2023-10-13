-- Tabla Propietarios
CREATE TABLE Propietarios (
  id INT IDENTITY(1,1) PRIMARY KEY,
  apellido VARCHAR(50),
  nombre VARCHAR(50)
);

-- Tabla Propiedades
CREATE TABLE Propiedades (
  id INT IDENTITY(1,1) PRIMARY KEY,
  calle VARCHAR(100),
  altura VARCHAR(10), 
  idPropietario INT, 
  FOREIGN KEY (idPropietario) REFERENCES Propietarios(id)
);

-- Tabla Alquileres
CREATE TABLE Alquileres (
  id INT IDENTITY(1,1) PRIMARY KEY,
  idPropiedad INT,
  monto DECIMAL(10,2)
);

INSERT INTO Propietarios (apellido, nombre) VALUES
('Perez', 'Juan'),
('Gonzalez', 'Maria'),
('Lopez', 'Carlos');

INSERT INTO Propiedades (calle, altura, idPropietario) VALUES
('Calle 1', '123', 1), 
('Calle 2', '456', 2),
('Calle 3', '789', 3);

INSERT INTO Alquileres (idPropiedad, monto) VALUES
(1, 1500.50), 
(2, 1200.75),
(3, 1800.25);