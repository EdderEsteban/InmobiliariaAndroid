-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 23-10-2024 a las 14:37:03
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliarianetandroid_edder`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `id_contrato` int(11) NOT NULL,
  `id_inquilino` int(11) DEFAULT NULL,
  `monto` int(11) NOT NULL,
  `id_inmueble` int(11) DEFAULT NULL,
  `fecha_inicio` date DEFAULT NULL,
  `fecha_fin` date DEFAULT NULL,
  `vigencia` tinyint(1) NOT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `fecha` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`id_contrato`, `id_inquilino`, `monto`, `id_inmueble`, `fecha_inicio`, `fecha_fin`, `vigencia`, `id_usuario`, `fecha`) VALUES
(14, 1, 330000, 1, '2024-04-01', '2025-04-30', 1, NULL, '2024-04-25 00:42:44'),
(15, 6, 540000, 8, '2024-01-01', '2024-12-31', 1, NULL, '2024-04-25 22:21:47'),
(16, 8, 700000, 5, '2024-01-01', '2024-12-31', 1, NULL, '2024-04-26 03:38:38'),
(17, 22, 118, 38, '2024-03-13', '2023-08-24', 0, NULL, '2024-08-27 23:08:00'),
(18, 22, 389, 42, '2023-12-25', '2024-06-25', 0, NULL, '2024-08-27 23:08:00'),
(19, 18, 497, 95, '2023-08-30', '2024-02-05', 0, NULL, '2024-08-27 23:08:00'),
(21, 39, 491, 58, '2024-07-10', '2024-03-03', 0, NULL, '2024-08-27 23:08:00'),
(22, 34, 945, 3, '2024-08-14', '2024-04-27', 0, NULL, '2024-08-27 23:08:01'),
(23, 41, 793, 81, '2024-06-26', '2024-03-12', 0, NULL, '2024-08-27 23:08:01'),
(24, 42, 593, 59, '2023-09-08', '2024-08-20', 0, NULL, '2024-08-27 23:08:01'),
(25, 17, 981, 19, '2023-09-01', '2024-02-24', 0, NULL, '2024-08-27 23:08:01'),
(26, 29, 995, 29, '2023-12-22', '2024-05-01', 0, NULL, '2024-08-27 23:08:01'),
(27, 72, 222, 61, '2023-12-12', '2023-09-24', 0, NULL, '2024-08-27 23:08:01'),
(28, 30, 813, 95, '2023-12-03', '2024-04-25', 0, NULL, '2024-08-27 23:08:01'),
(29, 29, 329, 66, '2023-11-28', '2024-02-25', 0, NULL, '2024-08-27 23:08:01'),
(30, 24, 767, 42, '2024-02-19', '2023-09-15', 0, NULL, '2024-08-27 23:08:01'),
(31, 51, 733, 97, '2023-10-09', '2024-06-10', 0, NULL, '2024-08-27 23:08:01'),
(32, 28, 729, 48, '2024-05-29', '2024-06-07', 0, NULL, '2024-08-27 23:08:01'),
(33, 31, 762, 5, '2024-07-08', '2024-05-15', 0, NULL, '2024-08-27 23:08:01'),
(34, 28, 235, 89, '2024-04-23', '2024-07-19', 0, NULL, '2024-08-27 23:08:01'),
(35, 28, 311, 90, '2024-01-02', '2024-07-31', 0, NULL, '2024-08-27 23:08:01'),
(36, 56, 762, 7, '2024-04-18', '2023-11-13', 0, NULL, '2024-08-27 23:08:01'),
(37, 29, 639, 32, '2024-05-11', '2023-08-31', 0, NULL, '2024-08-27 23:08:01'),
(38, 47, 251, 36, '2024-07-28', '2024-05-22', 0, NULL, '2024-08-27 23:08:01'),
(39, 61, 731, 61, '2023-11-13', '2024-05-23', 0, NULL, '2024-08-27 23:08:01'),
(40, 29, 865, 56, '2024-02-23', '2023-11-03', 0, NULL, '2024-08-27 23:08:01'),
(41, 36, 901, 20, '2023-08-31', '2023-10-24', 0, NULL, '2024-08-27 23:08:01'),
(42, 52, 790, 16, '2024-01-27', '2024-07-13', 0, NULL, '2024-08-27 23:08:01'),
(43, 59, 493, 65, '2024-04-06', '2024-02-05', 0, NULL, '2024-08-27 23:08:01'),
(44, 10, 604, 90, '2023-10-11', '2024-07-08', 0, NULL, '2024-08-27 23:08:01'),
(45, 42, 737, 82, '2024-04-05', '2024-05-17', 0, NULL, '2024-08-27 23:08:01'),
(46, 73, 914, 70, '2024-08-12', '2023-12-20', 0, NULL, '2024-08-27 23:08:01'),
(47, 31, 307, 41, '2023-09-04', '2024-05-08', 0, NULL, '2024-08-27 23:08:01'),
(48, 39, 431, 40, '2024-04-02', '2023-09-18', 0, NULL, '2024-08-27 23:08:02'),
(49, 74, 128, 22, '2023-12-28', '2024-03-15', 0, NULL, '2024-08-27 23:08:02'),
(50, 32, 904, 46, '2024-04-29', '2024-07-13', 0, NULL, '2024-08-27 23:08:02'),
(51, 54, 258, 75, '2024-01-10', '2023-12-02', 0, NULL, '2024-08-27 23:08:02'),
(52, 24, 383, 51, '2024-02-06', '2023-10-30', 0, NULL, '2024-08-27 23:08:02'),
(53, 22, 290, 56, '2024-01-25', '2024-03-17', 0, NULL, '2024-08-27 23:08:02'),
(54, 75, 945, 64, '2024-01-23', '2024-08-16', 0, NULL, '2024-08-27 23:08:02'),
(55, 74, 130, 67, '2024-01-01', '2024-01-02', 0, NULL, '2024-08-27 23:08:02'),
(56, 41, 381, 83, '2023-10-29', '2024-02-09', 0, NULL, '2024-08-27 23:08:02'),
(58, 21, 995, 45, '2023-10-14', '2024-01-03', 0, NULL, '2024-08-27 23:08:02'),
(59, 57, 506, 32, '2024-07-05', '2024-07-15', 0, NULL, '2024-08-27 23:08:02'),
(60, 46, 214, 77, '2023-09-14', '2024-08-14', 0, NULL, '2024-08-27 23:08:02'),
(61, 73, 323, 85, '2024-05-10', '2024-05-20', 0, NULL, '2024-08-27 23:08:02'),
(62, 31, 715, 75, '2024-06-05', '2023-12-08', 0, NULL, '2024-08-27 23:08:02'),
(64, 28, 675, 30, '2024-07-13', '2023-10-20', 0, NULL, '2024-08-27 23:08:02'),
(65, 47, 361, 93, '2024-06-09', '2023-09-27', 0, NULL, '2024-08-27 23:08:02'),
(66, 34, 248, 16, '2023-09-11', '2024-02-11', 0, NULL, '2024-08-27 23:08:02'),
(67, 17, 138, 93, '2024-04-11', '2024-04-07', 0, NULL, '2024-08-27 23:08:03'),
(68, 22, 118, 38, '2024-03-13', '2023-08-24', 0, NULL, '2024-08-27 23:09:10'),
(69, 22, 389, 42, '2023-12-25', '2024-06-25', 0, NULL, '2024-08-27 23:09:10'),
(70, 18, 497, 95, '2023-08-30', '2024-02-05', 0, NULL, '2024-08-27 23:09:10'),
(72, 39, 491, 58, '2024-07-10', '2024-03-03', 0, NULL, '2024-08-27 23:09:10'),
(73, 34, 945, 3, '2024-08-14', '2024-04-27', 0, NULL, '2024-08-27 23:09:10'),
(74, 41, 793, 81, '2024-06-26', '2024-03-12', 0, NULL, '2024-08-27 23:09:10'),
(75, 42, 593, 59, '2023-09-08', '2024-08-20', 0, NULL, '2024-08-27 23:09:10'),
(76, 17, 981, 19, '2023-09-01', '2024-02-24', 0, NULL, '2024-08-27 23:09:10'),
(77, 29, 995, 29, '2023-12-22', '2024-05-01', 0, NULL, '2024-08-27 23:09:10'),
(78, 72, 222, 61, '2023-12-12', '2023-09-24', 0, NULL, '2024-08-27 23:09:10'),
(79, 30, 813, 95, '2023-12-03', '2024-04-25', 0, NULL, '2024-08-27 23:09:10'),
(80, 29, 329, 66, '2023-11-28', '2024-02-25', 0, NULL, '2024-08-27 23:09:10'),
(81, 24, 767, 42, '2024-02-19', '2023-09-15', 0, NULL, '2024-08-27 23:09:10'),
(82, 51, 733, 97, '2023-10-09', '2024-06-10', 0, NULL, '2024-08-27 23:09:10'),
(83, 28, 729, 48, '2024-05-29', '2024-06-07', 0, NULL, '2024-08-27 23:09:10'),
(84, 31, 762, 5, '2024-07-08', '2024-05-15', 0, NULL, '2024-08-27 23:09:11'),
(85, 28, 235, 89, '2024-04-23', '2024-07-19', 0, NULL, '2024-08-27 23:09:11'),
(86, 28, 311, 90, '2024-01-02', '2024-07-31', 0, NULL, '2024-08-27 23:09:11'),
(87, 56, 762, 7, '2024-04-18', '2023-11-13', 0, NULL, '2024-08-27 23:09:11'),
(88, 29, 639, 32, '2024-05-11', '2023-08-31', 0, NULL, '2024-08-27 23:09:11'),
(89, 47, 251, 36, '2024-07-28', '2024-05-22', 0, NULL, '2024-08-27 23:09:11'),
(90, 61, 731, 61, '2023-11-13', '2024-05-23', 0, NULL, '2024-08-27 23:09:11'),
(91, 29, 865, 56, '2024-02-23', '2023-11-03', 0, NULL, '2024-08-27 23:09:11'),
(92, 36, 901, 20, '2023-08-31', '2023-10-24', 0, NULL, '2024-08-27 23:09:11'),
(93, 52, 790, 16, '2024-01-27', '2024-07-13', 0, NULL, '2024-08-27 23:09:11'),
(94, 59, 493, 65, '2024-04-06', '2024-02-05', 0, NULL, '2024-08-27 23:09:11'),
(95, 10, 604, 90, '2023-10-11', '2024-07-08', 0, NULL, '2024-08-27 23:09:11'),
(96, 42, 737, 82, '2024-04-05', '2024-05-17', 0, NULL, '2024-08-27 23:09:11'),
(97, 73, 914, 70, '2024-08-12', '2023-12-20', 0, NULL, '2024-08-27 23:09:11'),
(98, 31, 307, 41, '2023-09-04', '2024-05-08', 0, NULL, '2024-08-27 23:09:11'),
(99, 39, 431, 40, '2024-04-02', '2023-09-18', 0, NULL, '2024-08-27 23:09:11'),
(100, 74, 128, 22, '2023-12-28', '2024-03-15', 0, NULL, '2024-08-27 23:09:11'),
(101, 32, 904, 46, '2024-04-29', '2024-07-13', 0, NULL, '2024-08-27 23:09:11'),
(102, 54, 258, 75, '2024-01-10', '2023-12-02', 0, NULL, '2024-08-27 23:09:11'),
(103, 24, 383, 51, '2024-02-06', '2023-10-30', 0, NULL, '2024-08-27 23:09:11'),
(104, 22, 290, 56, '2024-01-25', '2024-03-17', 0, NULL, '2024-08-27 23:09:11'),
(105, 75, 945, 64, '2024-01-23', '2024-08-16', 0, NULL, '2024-08-27 23:09:11'),
(106, 74, 130, 67, '2024-01-01', '2024-01-02', 0, NULL, '2024-08-27 23:09:11'),
(107, 41, 381, 83, '2023-10-29', '2024-02-09', 0, NULL, '2024-08-27 23:09:11'),
(109, 21, 995, 45, '2023-10-14', '2024-01-03', 0, NULL, '2024-08-27 23:09:11'),
(110, 57, 506, 32, '2024-07-05', '2024-07-15', 0, NULL, '2024-08-27 23:09:11'),
(111, 46, 214, 77, '2023-09-14', '2024-08-14', 0, NULL, '2024-08-27 23:09:11'),
(112, 73, 323, 85, '2024-05-10', '2024-05-20', 0, NULL, '2024-08-27 23:09:11'),
(113, 31, 715, 75, '2024-06-05', '2023-12-08', 0, NULL, '2024-08-27 23:09:11'),
(115, 28, 675, 30, '2024-07-13', '2023-10-20', 0, NULL, '2024-08-27 23:09:12'),
(116, 47, 361, 93, '2024-06-09', '2023-09-27', 0, NULL, '2024-08-27 23:09:12'),
(117, 34, 248, 16, '2023-09-11', '2024-02-11', 0, NULL, '2024-08-27 23:09:12'),
(118, 17, 138, 93, '2024-04-11', '2024-04-07', 0, NULL, '2024-08-27 23:09:12'),
(119, 1, 300000, 17, '2024-01-01', '2024-08-31', 0, NULL, '2024-09-23 00:32:32'),
(121, 1, 300000, 3, '2024-01-01', '2024-08-31', 0, NULL, '2024-09-23 00:35:00'),
(122, 1, 300000, 98, '2024-09-01', '2024-12-31', 1, NULL, '2024-09-23 00:37:17');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `id_inmueble` int(11) NOT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  `uso` varchar(25) DEFAULT NULL,
  `id_tipo` int(11) DEFAULT NULL,
  `cantidad_ambientes` int(11) DEFAULT NULL,
  `precio_alquiler` decimal(10,2) DEFAULT NULL,
  `latitud` varchar(20) DEFAULT NULL,
  `longitud` varchar(20) DEFAULT NULL,
  `activo` tinyint(4) NOT NULL,
  `disponible` tinyint(4) NOT NULL,
  `inmuebleimg` varchar(255) DEFAULT NULL,
  `id_propietario` int(11) DEFAULT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `fecha` datetime NOT NULL DEFAULT current_timestamp(),
  `borrado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`id_inmueble`, `direccion`, `uso`, `id_tipo`, `cantidad_ambientes`, `precio_alquiler`, `latitud`, `longitud`, `activo`, `disponible`, `inmuebleimg`, `id_propietario`, `id_usuario`, `fecha`, `borrado`) VALUES
(1, 'Pringles 330 Dpto 3', 'Residencial', 1, 3, 230.00, '-33.30107764626808', '-66.32826228920763', 1, 0, NULL, 3, NULL, '2024-04-03 23:16:50', 0),
(2, 'Maipu 670', 'Comercial', 1, 3, 63000.00, '-33.30228322937067', '-66.33193927790455', 1, 0, NULL, 4, NULL, '2024-04-10 00:43:56', 1),
(3, 'Concaran', 'Residencial', 1, 1, 56000.00, '-33.302314597600216', '-66.33191967583032', 1, 0, NULL, 1, NULL, '2024-04-12 16:58:33', 0),
(5, 'Merlo', 'Comercial', 1, 3, 68000.00, '-33.30228322937067', '-66.33193927790455', 1, 0, NULL, 3, NULL, '2024-04-12 22:25:52', 0),
(7, 'Santiago', 'Comercial', 1, 1, 900000.00, '-33.30228322937067', '-66.33193927790455', 1, 0, NULL, 4, NULL, '2024-04-13 01:02:44', 0),
(8, 'La Punta', 'Residencial', 1, 4, 56000.00, '-33.30228322937067', '-66.31959623517908', 1, 0, NULL, 1, NULL, '2024-04-25 00:01:35', 0),
(9, '91510 Hermina Junction', 'Residencial', 3, 2, 0.00, '-20.9829904', '-40.9990597', 0, 0, NULL, 105, NULL, '2024-08-27 23:02:22', 0),
(10, '95 Eastlawn Street', 'Residencial', 5, 1, 0.00, '5.668899', '-72.994095', 1, 1, NULL, 105, NULL, '2024-08-27 23:02:23', 0),
(11, '3482 Emmet Circle', 'Comercial', 1, 1, 0.00, '-6.8055934', '110.7629067', 1, 1, NULL, 33, NULL, '2024-08-27 23:02:23', 1),
(12, '70827 Brickson Park Terrace', 'Residencial', 5, 1, 0.00, '39.639488', '110.606483', 0, 1, NULL, 105, NULL, '2024-08-27 23:02:23', 0),
(13, '03 Summer Ridge Alley', 'Comercial', 3, 3, 0.00, '41.5590095', '82.6211533', 0, 1, NULL, 12, NULL, '2024-08-27 23:02:23', 1),
(14, '5955 Orin Hill', 'Residencial', 4, 4, 0.00, '35.83339', '102.6395', 0, 0, NULL, 46, NULL, '2024-08-27 23:02:23', 1),
(15, '3 International Circle', 'Comercial', 2, 3, 0.00, '41.117916', '19.610198', 1, 0, NULL, 44, NULL, '2024-08-27 23:02:23', 0),
(16, '372 Iowa Point', 'Comercial', 5, 3, 0.00, '18.0435607', '100.1031202', 1, 1, NULL, 21, NULL, '2024-08-27 23:02:23', 0),
(17, '0396 Carberry Drive', 'Comercial', 3, 2, 0.00, '47.2523491', '28.282907', 1, 0, NULL, 47, NULL, '2024-08-27 23:02:23', 0),
(18, '5392 Schiller Center', 'Comercial', 1, 1, 0.00, '55.6817886', '12.5375058', 0, 1, NULL, 13, NULL, '2024-08-27 23:02:23', 0),
(19, '197 Heffernan Way', 'Residencial', 1, 4, 0.00, '46.748522', '82.978928', 1, 1, NULL, 23, NULL, '2024-08-27 23:02:23', 0),
(20, '61 Dakota Way', 'Comercial', 3, 2, 0.00, '47.0776249', '28.1792198', 1, 1, NULL, 31, NULL, '2024-08-27 23:02:23', 0),
(21, '5550 Oak Valley Terrace', 'Comercial', 1, 3, 0.00, '-7.0385115', '108.6561601', 1, 0, NULL, 20, NULL, '2024-08-27 23:02:23', 0),
(22, '9639 Sommers Place', 'Residencial', 6, 4, 0.00, '52.0424689', '21.3991045', 0, 0, NULL, 40, NULL, '2024-08-27 23:02:23', 0),
(23, '5 Summit Crossing', 'Residencial', 6, 1, 0.00, '3.5233855', '125.5110513', 1, 0, NULL, 18, NULL, '2024-08-27 23:02:23', 0),
(24, '092 Bluejay Place', 'Comercial', 4, 2, 0.00, '49.8691711', '22.9586235', 0, 1, NULL, 33, NULL, '2024-08-27 23:02:23', 0),
(25, '110 American Ash Terrace', 'Residencial', 4, 4, 0.00, '-10.8512419', '-36.970226', 1, 0, NULL, 20, NULL, '2024-08-27 23:02:23', 0),
(26, '7047 Dahle Place', 'Residencial', 3, 1, 0.00, '53.3284298', '-6.3048641', 0, 0, NULL, 15, NULL, '2024-08-27 23:02:24', 0),
(27, '17080 Anniversary Street', 'Residencial', 4, 3, 0.00, '35.7017899', '59.8468432', 0, 0, NULL, 17, NULL, '2024-08-27 23:02:24', 0),
(28, '811 Green Ridge Street', 'Residencial', 4, 1, 0.00, '42.2531168', '143.3092266', 1, 1, NULL, 46, NULL, '2024-08-27 23:02:24', 0),
(29, '752 Calypso Terrace', 'Comercial', 3, 2, 0.00, '-31.4774881', '-64.2278386', 1, 0, NULL, 11, NULL, '2024-08-27 23:02:24', 0),
(30, '19 Nancy Way', 'Comercial', 1, 1, 0.00, '2.1883402', '-76.2131474', 1, 1, NULL, 49, NULL, '2024-08-27 23:02:24', 0),
(31, '595 Schmedeman Avenue', 'Comercial', 3, 1, 0.00, '35.4660512', '136.236802', 0, 0, NULL, 36, NULL, '2024-08-27 23:02:24', 0),
(32, '83224 Dottie Circle', 'Comercial', 3, 1, 0.00, '17.1056791', '120.4557309', 1, 1, NULL, 42, NULL, '2024-08-27 23:02:24', 0),
(33, '327 Nevada Place', 'Comercial', 6, 4, 0.00, '-30.7875929', '-57.7771566', 0, 1, NULL, 49, NULL, '2024-08-27 23:02:24', 0),
(34, '60152 Nobel Point', 'Residencial', 1, 4, 0.00, '42.3894673', '23.7275817', 1, 0, NULL, 36, NULL, '2024-08-27 23:02:24', 0),
(35, '6079 Lakeland Plaza', 'Comercial', 4, 2, 0.00, '49.2917766', '31.4465327', 0, 0, NULL, 35, NULL, '2024-08-27 23:02:24', 0),
(36, '67 Ronald Regan Point', 'Residencial', 4, 2, 0.00, '45.9015821', '13.8803513', 0, 0, NULL, 31, NULL, '2024-08-27 23:02:24', 0),
(37, '047 Village Green Circle', 'Comercial', 6, 2, 0.00, '69.0474473', '33.2898473', 1, 0, NULL, 13, NULL, '2024-08-27 23:02:24', 0),
(38, '09651 American Ash Place', 'Residencial', 4, 3, 0.00, '36.1093631', '103.694265', 0, 1, NULL, 21, NULL, '2024-08-27 23:02:24', 0),
(39, '67784 Meadow Valley Park', 'Comercial', 2, 3, 0.00, '7.1949432', '100.5905788', 1, 1, NULL, 24, NULL, '2024-08-27 23:02:24', 0),
(40, '7645 Nelson Plaza', 'Comercial', 6, 3, 0.00, '-23.1157079', '-52.0851258', 1, 1, NULL, 22, NULL, '2024-08-27 23:02:24', 0),
(41, '1671 Union Drive', 'Residencial', 5, 4, 0.00, '-8.0321', '112.7975', 1, 1, NULL, 25, NULL, '2024-08-27 23:02:24', 0),
(42, '9 Loomis Drive', 'Residencial', 4, 3, 0.00, '35.3465609', '136.313308', 1, 0, NULL, 47, NULL, '2024-08-27 23:02:24', 0),
(43, '9575 Kennedy Place', 'Residencial', 6, 4, 0.00, '32.03144', '35.19992', 0, 0, NULL, 21, NULL, '2024-08-27 23:02:24', 0),
(44, '76 Dovetail Way', 'Residencial', 1, 1, 0.00, '4.7703172', '-76.0623979', 0, 0, NULL, 41, NULL, '2024-08-27 23:02:24', 0),
(45, '0277 Melrose Trail', 'Residencial', 3, 1, 0.00, '41.884195', '22.505053', 1, 0, NULL, 20, NULL, '2024-08-27 23:02:24', 0),
(46, '0 Fulton Lane', 'Residencial', 6, 4, 0.00, '-8.0972', '111.8163', 0, 0, NULL, 20, NULL, '2024-08-27 23:02:24', 0),
(47, '0 Brown Drive', 'Comercial', 6, 1, 0.00, '-7.2578991', '107.8632023', 0, 1, NULL, 15, NULL, '2024-08-27 23:02:24', 0),
(48, '8460 Jay Place', 'Comercial', 4, 2, 0.00, '59.3703069', '16.5529968', 1, 0, NULL, 47, NULL, '2024-08-27 23:02:24', 0),
(49, '8977 Lillian Terrace', 'Residencial', 2, 1, 0.00, '12.9978739', '121.4181928', 1, 0, NULL, 24, NULL, '2024-08-27 23:02:24', 0),
(50, '269 Fordem Parkway', 'Comercial', 1, 2, 0.00, '-7.6185371', '110.6013327', 0, 1, NULL, 24, NULL, '2024-08-27 23:02:24', 0),
(51, '13792 Continental Point', 'Residencial', 2, 3, 0.00, '54.0541924', '17.5871593', 0, 0, NULL, 41, NULL, '2024-08-27 23:02:24', 0),
(52, '528 Tennyson Terrace', 'Comercial', 4, 4, 0.00, '-24.9250707', '27.6170798', 0, 1, NULL, 32, NULL, '2024-08-27 23:02:24', 0),
(53, '6674 Ridge Oak Street', 'Comercial', 5, 3, 0.00, '41.4198927', '-8.5917806', 1, 0, NULL, 29, NULL, '2024-08-27 23:02:24', 0),
(54, '75909 Kinsman Point', 'Comercial', 6, 4, 0.00, '43.1839958', '16.5992505', 0, 1, NULL, 31, NULL, '2024-08-27 23:02:24', 0),
(55, '3 Helena Center', 'Residencial', 3, 1, 0.00, '34.682497', '103.359019', 0, 1, NULL, 37, NULL, '2024-08-27 23:02:24', 0),
(56, '19 Norway Maple Center', 'Comercial', 2, 3, 0.00, '40.4751843', '-8.6970168', 1, 1, NULL, 40, NULL, '2024-08-27 23:02:24', 0),
(57, '4 Onsgard Pass', 'Comercial', 3, 3, 0.00, '43.1097139', '17.7192104', 1, 0, NULL, 10, NULL, '2024-08-27 23:02:24', 0),
(58, '68 Orin Drive', 'Residencial', 2, 4, 0.00, '10.335192', '123.3262483', 0, 1, NULL, 34, NULL, '2024-08-27 23:02:24', 0),
(59, '22275 Anniversary Park', 'Comercial', 1, 2, 0.00, '51.0176058', '15.677587', 1, 0, NULL, 13, NULL, '2024-08-27 23:02:24', 0),
(60, '72045 Loeprich Street', 'Residencial', 5, 2, 0.00, '45.9469236', '20.1591006', 0, 0, NULL, 24, NULL, '2024-08-27 23:02:24', 0),
(61, '7592 Westridge Plaza', 'Comercial', 2, 3, 0.00, '35.819227', '108.019549', 1, 1, NULL, 26, NULL, '2024-08-27 23:02:24', 0),
(62, '42266 Eliot Trail', 'Comercial', 1, 4, 0.00, '26.396895', '118.449395', 1, 0, NULL, 14, NULL, '2024-08-27 23:02:25', 0),
(63, '4 Cascade Lane', 'Residencial', 5, 3, 0.00, '-20.9969938', '-63.3933089', 1, 1, NULL, 23, NULL, '2024-08-27 23:02:25', 0),
(64, '70737 Darwin Parkway', 'Comercial', 6, 2, 0.00, '15.17627', '-88.724632', 0, 0, NULL, 34, NULL, '2024-08-27 23:02:25', 0),
(65, '5 Dottie Parkway', 'Comercial', 2, 1, 0.00, '60.5075418', '14.9464129', 0, 0, NULL, 49, NULL, '2024-08-27 23:02:25', 0),
(66, '6 Logan Circle', 'Comercial', 4, 4, 0.00, '58.703557', '14.1362521', 0, 1, NULL, 28, NULL, '2024-08-27 23:02:25', 0),
(67, '9 Sutherland Terrace', 'Residencial', 2, 1, 0.00, '11.9050612', '-86.0962783', 0, 1, NULL, 24, NULL, '2024-08-27 23:02:25', 0),
(68, '19368 Shoshone Court', 'Comercial', 5, 4, 0.00, '48.9969392', '31.4022422', 1, 1, NULL, 26, NULL, '2024-08-27 23:02:25', 0),
(69, '2605 Longview Circle', 'Comercial', 1, 1, 0.00, '-7.1861216', '108.4424414', 1, 0, NULL, 20, NULL, '2024-08-27 23:02:25', 0),
(70, '96 New Castle Crossing', 'Comercial', 1, 2, 0.00, '30.20003', '115.038835', 0, 0, NULL, 46, NULL, '2024-08-27 23:02:25', 0),
(71, '64962 Anniversary Plaza', 'Residencial', 4, 1, 0.00, '-31.3927967', '-64.2545133', 0, 0, NULL, 28, NULL, '2024-08-27 23:02:25', 0),
(72, '4529 Esker Junction', 'Residencial', 4, 2, 0.00, '-15.25055', '-71.0927279', 1, 1, NULL, 14, NULL, '2024-08-27 23:02:25', 0),
(73, '34553 Springs Park', 'Residencial', 4, 1, 0.00, '41.6493465', '22.1713543', 1, 1, NULL, 21, NULL, '2024-08-27 23:02:25', 0),
(74, '4 Linden Center', 'Comercial', 5, 1, 0.00, '38.504559', '125.4946236', 1, 1, NULL, 49, NULL, '2024-08-27 23:02:25', 0),
(75, '5168 Prairieview Way', 'Residencial', 3, 4, 0.00, '40.0480719', '20.7528378', 0, 1, NULL, 10, NULL, '2024-08-27 23:02:25', 0),
(76, '1 Grover Junction', 'Residencial', 2, 1, 0.00, '50.655135', '18.3329704', 0, 1, NULL, 29, NULL, '2024-08-27 23:02:25', 0),
(77, '20 Westridge Road', 'Residencial', 2, 1, 0.00, '20.8965838', '-105.4093552', 1, 0, NULL, 48, NULL, '2024-08-27 23:02:25', 0),
(78, '65 Luster Drive', 'Comercial', 3, 2, 0.00, '45.7653181', '20.2863213', 0, 0, NULL, 48, NULL, '2024-08-27 23:02:25', 0),
(79, '72414 Ramsey Way', 'Residencial', 2, 3, 0.00, '36.68333', '71.53333', 0, 1, NULL, 17, NULL, '2024-08-27 23:02:25', 0),
(80, '34 Fair Oaks Pass', 'Residencial', 4, 3, 0.00, '30.4158383', '31.5621184', 1, 1, NULL, 37, NULL, '2024-08-27 23:02:25', 0),
(81, '96541 Algoma Avenue', 'Residencial', 6, 3, 0.00, '21.027964', '105.8510132', 0, 1, NULL, 50, NULL, '2024-08-27 23:02:25', 0),
(82, '33 Gerald Center', 'Comercial', 4, 4, 0.00, '42.2214597', '43.9644051', 1, 1, NULL, 28, NULL, '2024-08-27 23:02:25', 0),
(83, '8822 Harper Way', 'Residencial', 3, 2, 0.00, '-22.817412', '47.818038', 0, 0, NULL, 17, NULL, '2024-08-27 23:02:25', 0),
(84, '69777 Lotheville Place', 'Comercial', 3, 1, 0.00, '20.3814569', '105.1171246', 0, 1, NULL, 18, NULL, '2024-08-27 23:02:25', 0),
(85, '50349 School Drive', 'Residencial', 3, 4, 0.00, '-14.07546', '-75.7341811', 0, 1, NULL, 43, NULL, '2024-08-27 23:02:25', 0),
(86, '76968 Memorial Trail', 'Residencial', 1, 2, 0.00, '35.78333', '10.8', 1, 0, NULL, 44, NULL, '2024-08-27 23:02:25', 0),
(87, '7720 Manufacturers Crossing', 'Comercial', 3, 1, 0.00, '47.372696', '132.145184', 1, 0, NULL, 10, NULL, '2024-08-27 23:02:25', 0),
(88, '867 Mitchell Lane', 'Comercial', 5, 2, 0.00, '-23.3058194', '-47.1367011', 0, 0, NULL, 15, NULL, '2024-08-27 23:02:25', 0),
(89, '4310 Buell Crossing', 'Residencial', 6, 2, 0.00, '45.0934491', '-73.9764283', 1, 1, NULL, 40, NULL, '2024-08-27 23:02:26', 0),
(90, '18 Vera Court', 'Comercial', 3, 3, 0.00, '30.789876', '103.455954', 0, 1, NULL, 26, NULL, '2024-08-27 23:02:26', 0),
(91, '3119 Laurel Park', 'Comercial', 5, 2, 0.00, '14.5701829', '121.0202323', 0, 1, NULL, 22, NULL, '2024-08-27 23:02:26', 0),
(92, '570 Sycamore Lane', 'Comercial', 2, 4, 0.00, '57.9679793', '31.6515246', 1, 1, NULL, 35, NULL, '2024-08-27 23:02:26', 0),
(93, '0 Banding Crossing', 'Comercial', 4, 3, 0.00, '-13.589688', '-73.9962255', 1, 0, NULL, 18, NULL, '2024-08-27 23:02:26', 0),
(94, '92 Menomonie Center', 'Comercial', 1, 3, 0.00, '-32.5711982', '-64.3878273', 1, 1, NULL, 10, NULL, '2024-08-27 23:02:26', 0),
(95, '61 Mendota Trail', 'Residencial', 3, 2, 0.00, '50.4810642', '34.9654363', 1, 0, NULL, 33, NULL, '2024-08-27 23:02:26', 0),
(96, '906 Clarendon Crossing', 'Comercial', 3, 2, 0.00, '37.7181047', '-25.372789', 1, 1, NULL, 43, NULL, '2024-08-27 23:02:26', 0),
(97, '25 Golf View Alley', 'Comercial', 3, 1, 0.00, '27.5800122', '-109.9297789', 1, 0, NULL, 43, NULL, '2024-08-27 23:02:26', 0),
(98, '09418 Fulton Center', 'Comercial', 5, 2, 0.00, '45.1150317', '15.5854843', 1, 0, NULL, 46, NULL, '2024-08-27 23:02:26', 0),
(99, '87 Vidon Circle', 'Residencial', 3, 4, 0.00, '50.1120523', '16.2564466', 1, 1, NULL, 29, NULL, '2024-08-27 23:02:26', 0),
(100, '84 Towne Lane', 'Comercial', 4, 1, 0.00, '38.0740965', '20.7968395', 0, 0, NULL, 32, NULL, '2024-08-27 23:02:26', 0),
(101, '700 Springview Parkway', 'Comercial', 1, 3, 0.00, '50.4163784', '-105.5361463', 1, 1, NULL, 26, NULL, '2024-08-27 23:02:26', 0),
(102, '3900 Glacier Hill Terrace', 'Residencial', 3, 3, 0.00, '15.60223', '44.0242', 0, 0, NULL, 35, NULL, '2024-08-27 23:02:26', 0),
(103, '116 Mallory Park', 'Comercial', 3, 4, 0.00, '9.6365101', '123.8536209', 0, 1, NULL, 46, NULL, '2024-08-27 23:02:26', 0),
(104, '398 Carpenter Hill', 'Residencial', 5, 1, 0.00, '39.964054', '116.714967', 0, 1, NULL, 27, NULL, '2024-08-27 23:02:26', 0),
(105, '9604 Farmco Hill', 'Comercial', 5, 4, 0.00, '50.2496386', '66.9140468', 1, 1, NULL, 11, NULL, '2024-08-27 23:02:26', 0),
(106, '4 Killdeer Drive', 'Residencial', 5, 3, 0.00, '56.0349544', '35.9510395', 0, 1, NULL, 28, NULL, '2024-08-27 23:02:26', 0),
(107, '5 Meadow Valley Way', 'Comercial', 3, 2, 0.00, '38.8426524', '-9.1289108', 0, 0, NULL, 17, NULL, '2024-08-27 23:02:26', 0),
(108, '817 Lawn Avenue', 'Comercial', 5, 3, 0.00, '40.9690916', '-8.474627', 1, 1, NULL, 32, NULL, '2024-08-27 23:02:26', 0),
(109, 'Maipu 670', 'Comercial', 2, 3, 56000.00, '-33.30228322937067', '-66.3319015570454', 1, 0, NULL, 4, NULL, '2024-09-11 00:19:30', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `id_inquilino` int(11) NOT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `apellido` varchar(255) DEFAULT NULL,
  `dni` varchar(8) NOT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `correo` varchar(255) DEFAULT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `fecha` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`id_inquilino`, `nombre`, `apellido`, `dni`, `direccion`, `telefono`, `correo`, `id_usuario`, `fecha`) VALUES
(1, 'Edder', 'Santibañez', '93962239', 'San Luis', '2664271316', 'edder@santy.com', NULL, '2024-04-02 00:00:00'),
(2, 'Diego', 'Orellano', '98765432', 'Santa Fe', '2665122345', 'diego@orellano.com', NULL, '2024-04-02 00:00:00'),
(6, 'Mauricio', 'Ferrieres', '45685212', 'San Luis', '2664854565', 'mauricio@ferrieres.com', NULL, '2024-04-02 00:00:00'),
(8, 'Fatima', 'Lebri', '29426271', 'San Luis', '2664718225', 'fatima@lebri.com', NULL, '2024-04-02 00:00:00'),
(10, 'Jose', 'Perez', '44333222', 'Concaran', '2664568923', 'jose@perez.com', NULL, '2024-04-02 00:00:00'),
(11, 'Mariya', 'Monnoyer', '98', '0 Beilfuss Center', '956-342-5574', 'mmonnoyer0@weather.com', NULL, '2024-08-25 02:05:52'),
(12, 'Natassia', 'Praton', '52', '86 Hazelcrest Parkway', '498-484-8651', 'npraton1@is.gd', NULL, '2024-08-25 02:05:53'),
(14, 'Cory', 'Trevan', '92', '613 Forest Run Street', '379-444-8015', 'ctrevan3@dailymail.co.uk', NULL, '2024-08-25 02:05:54'),
(15, 'Case', 'Colvill', '82', '140 Grayhawk Park', '319-117-2412', 'ccolvill4@posterous.com', NULL, '2024-08-25 02:05:54'),
(16, 'Keeley', 'Wenban', '8', '5188 East Trail', '740-815-2504', 'kwenban5@alexa.com', NULL, '2024-08-25 02:05:54'),
(17, 'Annelise', 'Billham', '79', '41 Sutteridge Way', '409-301-2548', 'abillham6@wsj.com', NULL, '2024-08-25 02:05:54'),
(18, 'Fonz', 'Rippon', '95', '871 Oakridge Junction', '175-934-1929', 'frippon7@irs.gov', NULL, '2024-08-25 02:05:54'),
(19, 'Kandace', 'Wrates', '34', '20 Dottie Pass', '773-855-5814', 'kwrates8@geocities.jp', NULL, '2024-08-25 02:05:54'),
(20, 'Allene', 'Spratt', '31', '565 Valley Edge Point', '443-271-6724', 'aspratt9@themeforest.net', NULL, '2024-08-25 02:05:54'),
(21, 'Frasco', 'Simonini', '94', '0 Grayhawk Terrace', '127-603-9322', 'fsimoninia@elpais.com', NULL, '2024-08-25 02:05:54'),
(22, 'Chrisy', 'Shervington', '47', '894 Del Mar Junction', '496-463-9343', 'cshervingtonb@foxnews.com', NULL, '2024-08-25 02:05:54'),
(23, 'Kaitlynn', 'Begbie', '55', '47627 Elgar Terrace', '612-468-7503', 'kbegbiec@bluehost.com', NULL, '2024-08-25 02:05:55'),
(24, 'Raquela', 'Cheese', '20', '61499 Loomis Avenue', '285-544-6464', 'rcheesed@army.mil', NULL, '2024-08-25 02:05:55'),
(25, 'Sapphira', 'Barthel', '86', '7799 Union Street', '297-726-9394', 'sbarthele@forbes.com', NULL, '2024-08-25 02:05:55'),
(26, 'Ki', 'Kellett', '93', '2614 Lukken Junction', '535-376-0757', 'kkellettf@amazonaws.com', NULL, '2024-08-25 02:05:55'),
(27, 'Lucas', 'Illston', '82', '33 7th Center', '240-374-7513', 'lillstong@topsy.com', NULL, '2024-08-25 02:05:55'),
(28, 'Cordie', 'Bendell', '37', '2754 Lakeland Street', '367-223-2219', 'cbendellh@bandcamp.com', NULL, '2024-08-25 02:05:55'),
(29, 'Julita', 'Coolican', '66', '22501 Eliot Center', '693-971-6647', 'jcoolicani@ibm.com', NULL, '2024-08-25 02:05:55'),
(30, 'Ynez', 'Trevan', '32', '30735 Truax Drive', '846-609-8426', 'ytrevanj@merriam-webster.com', NULL, '2024-08-25 02:05:56'),
(31, 'Brice', 'Shearston', '91', '32328 Springview Point', '127-947-9995', 'bshearstonk@japanpost.jp', NULL, '2024-08-25 02:05:56'),
(32, 'Dina', 'Chaudret', '18', '92 Village Green Drive', '176-859-6884', 'dchaudretl@pbs.org', NULL, '2024-08-25 02:05:56'),
(33, 'Wilone', 'Asty', '32', '67 Beilfuss Hill', '406-395-3045', 'wastym@ed.gov', NULL, '2024-08-25 02:05:56'),
(34, 'Claudelle', 'Flamank', '16', '53 Steensland Road', '461-328-9843', 'cflamankn@sourceforge.net', NULL, '2024-08-25 02:05:56'),
(35, 'Ulysses', 'Leatham', '96', '87683 Forster Court', '232-794-7633', 'uleathamo@ox.ac.uk', NULL, '2024-08-25 02:05:56'),
(36, 'Bret', 'Webberley', '65', '335 Oneill Center', '355-766-3499', 'bwebberleyp@lycos.com', NULL, '2024-08-25 02:05:56'),
(37, 'Ollie', 'MacMeanma', '25', '200 Haas Plaza', '378-972-6166', 'omacmeanmaq@w3.org', NULL, '2024-08-25 02:05:56'),
(38, 'Neilla', 'Antham', '11', '79681 Fordem Terrace', '368-283-9212', 'nanthamr@cmu.edu', NULL, '2024-08-25 02:05:56'),
(39, 'Blinni', 'Ambroisin', '20', '25 Caliangt Center', '911-589-4343', 'bambroisins@printfriendly.com', NULL, '2024-08-25 02:05:57'),
(40, 'Klemens', 'Farryn', '0', '496 Grasskamp Plaza', '483-782-9013', 'kfarrynt@fda.gov', NULL, '2024-08-25 02:05:57'),
(41, 'Griffie', 'Loan', '28', '6 Weeping Birch Street', '817-966-3911', 'gloanu@oaic.gov.au', NULL, '2024-08-25 02:05:57'),
(42, 'Kerby', 'Staig', '27', '4830 Sullivan Drive', '682-976-8076', 'kstaigv@mapquest.com', NULL, '2024-08-25 02:05:57'),
(43, 'Debera', 'Ducaen', '27', '710 Troy Pass', '814-809-3214', 'dducaenw@pen.io', NULL, '2024-08-25 02:05:57'),
(44, 'Reinhard', 'Burnett', '92', '849 Emmet Parkway', '322-903-2858', 'rburnettx@t.co', NULL, '2024-08-25 02:05:57'),
(45, 'Gregorius', 'Perrottet', '40', '03847 Bellgrove Terrace', '610-995-8422', 'gperrottety@europa.eu', NULL, '2024-08-25 02:05:57'),
(46, 'Amata', 'Murrells', '84', '71013 Kropf Alley', '273-656-8402', 'amurrellsz@howstuffworks.com', NULL, '2024-08-25 02:05:57'),
(47, 'Paolina', 'Lindstrom', '97', '30 Lunder Hill', '143-898-4203', 'plindstrom10@howstuffworks.com', NULL, '2024-08-25 02:05:57'),
(48, 'Caresa', 'Van der Son', '82', '1651 Rowland Court', '844-675-2674', 'cvanderson11@g.co', NULL, '2024-08-25 02:05:57'),
(49, 'Sholom', 'Hacon', '85', '27533 Texas Crossing', '512-743-0081', 'shacon12@myspace.com', NULL, '2024-08-25 02:05:57'),
(50, 'Idalina', 'Salvadori', '70', '235 Buena Vista Parkway', '839-517-4546', 'isalvadori13@shareasale.com', NULL, '2024-08-25 02:05:58'),
(51, 'Chip', 'Riddock', '15', '9917 Bashford Parkway', '267-171-0143', 'criddock14@msu.edu', NULL, '2024-08-25 02:05:58'),
(52, 'Papageno', 'Clementi', '44', '73166 South Parkway', '379-854-7188', 'pclementi15@utexas.edu', NULL, '2024-08-25 02:05:58'),
(53, 'Liza', 'Bullard', '78', '6272 Arizona Place', '174-311-5598', 'lbullard16@cbslocal.com', NULL, '2024-08-25 02:05:58'),
(54, 'Ailson', 'Dedham', '74962239', '5983 Crest Line Court', '225-288-6152', 'adedham17@list-manage.com', NULL, '2024-08-25 02:05:58'),
(55, 'Herc', 'Cristol', '3', '48225 Tennyson Crossing', '694-195-6791', 'hcristol18@goo.ne.jp', NULL, '2024-08-25 02:05:58'),
(56, 'Leighton', 'Rentilll', '63', '0043 Sunbrook Street', '359-385-5581', 'lrentilll19@dropbox.com', NULL, '2024-08-25 02:05:58'),
(57, 'Somerset', 'Tudball', '49', '08 Red Cloud Court', '872-303-2482', 'studball1a@pinterest.com', NULL, '2024-08-25 02:05:58'),
(58, 'Charyl', 'Backes', '65', '0 Michigan Court', '110-914-2750', 'cbackes1b@google.co.jp', NULL, '2024-08-25 02:05:59'),
(59, 'Reese', 'Durie', '61', '97226 Messerschmidt Avenue', '214-539-1300', 'rdurie1c@mysql.com', NULL, '2024-08-25 02:05:59'),
(60, 'Aleksandr', 'Frankiewicz', '14', '6283 Sherman Alley', '540-724-8885', 'afrankiewicz1d@cdbaby.com', NULL, '2024-08-25 02:05:59'),
(61, 'Edna', 'Ogan', '89', '6302 Gerald Pass', '292-130-0049', 'eogan1e@npr.org', NULL, '2024-08-25 02:05:59'),
(62, 'Anne-marie', 'Fattorini', '95', '92019 Meadow Vale Road', '339-971-7834', 'afattorini1f@google.co.jp', NULL, '2024-08-25 02:05:59'),
(63, 'Blithe', 'Babber', '98', '567 Forest Run Way', '409-880-5716', 'bbabber1g@home.pl', NULL, '2024-08-25 02:05:59'),
(64, 'Dante', 'Bodle', '75', '79520 Wayridge Point', '167-429-8775', 'dbodle1h@list-manage.com', NULL, '2024-08-25 02:05:59'),
(65, 'Cheston', 'McTurk', '3', '73958 High Crossing Trail', '615-766-6000', 'cmcturk1i@china.com.cn', NULL, '2024-08-25 02:05:59'),
(66, 'Philippe', 'Jacquemy', '64', '7 South Parkway', '548-861-6702', 'pjacquemy1j@cisco.com', NULL, '2024-08-25 02:05:59'),
(67, 'Mechelle', 'Waldocke', '61', '2117 Roth Trail', '420-952-2857', 'mwaldocke1k@wsj.com', NULL, '2024-08-25 02:05:59'),
(68, 'Kane', 'Rostron', '95', '645 Bluestem Parkway', '828-255-8621', 'krostron1l@tmall.com', NULL, '2024-08-25 02:05:59'),
(69, 'Bone', 'Kopfer', '50', '7 Garrison Alley', '690-593-7934', 'bkopfer1m@ameblo.jp', NULL, '2024-08-25 02:05:59'),
(70, 'Rowen', 'Woolger', '79', '27131 Stone Corner Drive', '830-996-7886', 'rwoolger1n@examiner.com', NULL, '2024-08-25 02:05:59'),
(71, 'Esther', 'Entwisle', '39', '7416 Northwestern Road', '375-896-9204', 'eentwisle1o@icio.us', NULL, '2024-08-25 02:05:59'),
(72, 'Damara', 'Skittreal', '70', '18012 Mifflin Avenue', '134-366-5146', 'dskittreal1p@yolasite.com', NULL, '2024-08-25 02:05:59'),
(73, 'Miranda', 'Butt', '14', '7593 Waxwing Crossing', '413-409-2287', 'mbutt1q@wikia.com', NULL, '2024-08-25 02:05:59'),
(74, 'Brittni', 'Grebert', '44', '05616 Fuller Alley', '119-461-0736', 'bgrebert1r@economist.com', NULL, '2024-08-25 02:05:59'),
(75, 'Ericka', 'Sturley', '39', '33666 Heffernan Plaza', '459-888-2692', 'esturley1s@msu.edu', NULL, '2024-08-25 02:05:59'),
(76, 'Rheba', 'Branigan', '45', '42292 Mayfield Road', '557-991-9249', 'rbranigan1t@europa.eu', NULL, '2024-08-25 02:05:59'),
(77, 'Evey', 'Dennehy', '90', '3228 Butternut Place', '153-761-1160', 'edennehy1u@woothemes.com', NULL, '2024-08-25 02:05:59'),
(78, 'Katti', 'Balmadier', '85', '64997 Annamark Junction', '940-897-4078', 'kbalmadier1v@ed.gov', NULL, '2024-08-25 02:05:59'),
(79, 'Arleta', 'Trubshawe', '1', '363 Eggendart Court', '401-107-8919', 'atrubshawe1w@earthlink.net', NULL, '2024-08-25 02:05:59'),
(80, 'Rudyard', 'Cratchley', '27', '513 Summer Ridge Court', '578-328-6795', 'rcratchley1x@gov.uk', NULL, '2024-08-25 02:05:59'),
(81, 'Judon', 'McWhinnie', '5', '38583 Schlimgen Hill', '930-586-1146', 'jmcwhinnie1y@europa.eu', NULL, '2024-08-25 02:05:59'),
(82, 'Alisa', 'Torri', '87', '4 Forest Terrace', '652-110-3996', 'atorri1z@samsung.com', NULL, '2024-08-25 02:05:59'),
(84, 'Jill', 'Frantsev', '40', '8 Roxbury Parkway', '257-140-8687', 'jfrantsev21@uol.com.br', NULL, '2024-08-25 02:06:00'),
(85, 'Blaire', 'Tamlett', '68', '8 Garrison Lane', '320-597-1881', 'btamlett22@apache.org', NULL, '2024-08-25 02:06:00'),
(86, 'Alastair', 'Simonitto', '56', '03 Comanche Plaza', '487-717-4578', 'asimonitto23@bloomberg.com', NULL, '2024-08-25 02:06:00'),
(87, 'Silas', 'Riccardini', '69', '48 Tony Court', '946-129-6924', 'sriccardini24@w3.org', NULL, '2024-08-25 02:06:00'),
(88, 'Any', 'Trever', '81', '7 David Circle', '979-131-7327', 'atrever25@spotify.com', NULL, '2024-08-25 02:06:00'),
(89, 'Jan', 'Wyke', '8', '8 Melody Trail', '356-476-2531', 'jwyke26@lycos.com', NULL, '2024-08-25 02:06:00'),
(90, 'Ambrose', 'Wynrahame', '41', '3 Hovde Center', '781-842-3677', 'awynrahame27@studiopress.com', NULL, '2024-08-25 02:06:00'),
(91, 'Sibylla', 'Baddeley', '41', '2630 Kropf Pass', '908-978-3568', 'sbaddeley28@tiny.cc', NULL, '2024-08-25 02:06:00'),
(92, 'Birgit', 'Legon', '2', '5197 Reinke Park', '925-630-2362', 'blegon29@yale.edu', NULL, '2024-08-25 02:06:00'),
(93, 'Gawen', 'Aps', '94', '47251 Forest Parkway', '346-158-5877', 'gaps2a@usgs.gov', NULL, '2024-08-25 02:06:00'),
(94, 'Allix', 'Guite', '88', '2 Luster Center', '550-322-4323', 'aguite2b@studiopress.com', NULL, '2024-08-25 02:06:00'),
(95, 'Jelene', 'Hanster', '27', '67 Eastwood Avenue', '766-190-8606', 'jhanster2c@java.com', NULL, '2024-08-25 02:06:00'),
(96, 'Nikolia', 'Cornelissen', '23', '0 Columbus Street', '542-956-5065', 'ncornelissen2d@reuters.com', NULL, '2024-08-25 02:06:00'),
(97, 'Trevar', 'Went', '77', '654 Moland Avenue', '876-827-0542', 'twent2e@ucoz.ru', NULL, '2024-08-25 02:06:00'),
(98, 'Ede', 'Yemm', '84', '2865 Scott Road', '457-715-4760', 'eyemm2f@upenn.edu', NULL, '2024-08-25 02:06:01'),
(99, 'Bettye', 'Dockerty', '68', '27860 Butterfield Trail', '881-716-1915', 'bdockerty2g@archive.org', NULL, '2024-08-25 02:06:01'),
(101, 'Claudius', 'Dwane', '48', '1629 Daystar Place', '915-267-9764', 'cdwane2i@hhs.gov', NULL, '2024-08-25 02:06:01'),
(102, 'Itch', 'Bartomeu', '45', '32847 Orin Parkway', '437-614-9838', 'ibartomeu2j@1und1.de', NULL, '2024-08-25 02:06:01'),
(103, 'Neila', 'McCaffrey', '13', '57 Coleman Court', '966-327-2375', 'nmccaffrey2k@oracle.com', NULL, '2024-08-25 02:06:01'),
(104, 'Renae', 'Coushe', '44', '75257 1st Junction', '845-223-2322', 'rcoushe2l@scribd.com', NULL, '2024-08-25 02:06:01'),
(105, 'Sallyanne', 'Jolliman', '57', '396 Memorial Place', '747-744-4187', 'sjolliman2m@sbwire.com', NULL, '2024-08-25 02:06:01'),
(106, 'Cheslie', 'Shewan', '86', '622 Springview Place', '213-491-2847', 'cshewan2n@typepad.com', NULL, '2024-08-25 02:06:01'),
(107, 'Modestia', 'Vayne', '30', '65 Fieldstone Terrace', '913-714-2088', 'mvayne2o@elegantthemes.com', NULL, '2024-08-25 02:06:01'),
(108, 'Ogden', 'Bisp', '71', '219 Onsgard Circle', '826-470-7564', 'obisp2p@cnbc.com', NULL, '2024-08-25 02:06:01'),
(109, 'Elaine', 'Greschke', '44', '514 Towne Place', '313-400-0524', 'egreschke2q@taobao.com', NULL, '2024-08-25 02:06:01'),
(110, 'Alejandra', 'Glassup', '71', '2 Parkside Point', '679-798-1023', 'aglassup2r@webnode.com', NULL, '2024-08-25 02:06:01'),
(111, 'Edder', 'Santibañez', '1111111', 'Caseros 633', '02664271316', 'prueba@borrado.com', NULL, '2024-08-26 00:16:56');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `id_pago` int(11) NOT NULL,
  `id_contrato` int(11) DEFAULT NULL,
  `fecha_pago` date DEFAULT NULL,
  `monto` decimal(10,2) DEFAULT NULL,
  `periodo` date DEFAULT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `id_inquilino` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pago`
--

INSERT INTO `pago` (`id_pago`, `id_contrato`, `fecha_pago`, `monto`, `periodo`, `id_usuario`, `id_inquilino`) VALUES
(10, 14, '2024-09-26', 150000.00, '2024-09-01', NULL, 1),
(11, 15, '2024-09-26', 300000.00, '2024-10-01', NULL, 6),
(12, 122, '2024-09-26', 150000.00, '2024-10-11', NULL, 1),
(13, 16, '2024-09-26', 300000.00, '2024-10-02', NULL, 8),
(14, 122, '2024-09-26', 300000.00, '2024-09-06', NULL, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `id_propietario` int(11) NOT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `apellido` varchar(255) DEFAULT NULL,
  `dni` varchar(8) NOT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `correo` varchar(255) DEFAULT NULL,
  `contraseña` varchar(255) NOT NULL,
  `avatar` varchar(255) DEFAULT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `fecha` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`id_propietario`, `nombre`, `apellido`, `dni`, `direccion`, `telefono`, `correo`, `contraseña`, `avatar`, `id_usuario`, `fecha`) VALUES
(1, 'Matias', 'Diaz', '12345678', 'Merlo', '2665122345', 'maty@diaz.com', '', '', NULL, '2024-04-02 00:02:25'),
(3, 'Simon', 'Ortega', '65498732', 'Salta', '2664857985', 'simon@ortega.com', '', '', NULL, '2024-04-09 22:50:48'),
(4, 'Edder', 'Santibañez', '93962239', 'Min Berrondo 338', '2664271316', 'edder@santi.com', 'P9jgAcHQ8uymuSeyBkJh+Nl3vP3vozXoXLmJHm8+63g=', '', 34, '2024-04-12 23:54:42'),
(5, 'Geralda', 'Brigdale', '19', '46481 Hooker Lane', '901-786-6670', 'gbrigdale0@umn.edu', '', '', NULL, '2024-08-20 20:59:50'),
(6, 'Janna', 'Tull', '25', '3 Ridge Oak Drive', '779-874-1761', 'jtull1@sfgate.com', '', '', NULL, '2024-08-20 20:59:51'),
(7, 'Linea', 'Proswell', '50', '3695 Johnson Way', '950-968-6588', 'lproswell2@goodreads.com', '', '', NULL, '2024-08-20 20:59:51'),
(8, 'Chilton', 'Cholerton', '58', '6 Commercial Junction', '727-179-2910', 'ccholerton3@vinaora.com', '', '', NULL, '2024-08-20 20:59:51'),
(9, 'Hoebart', 'Fontenot', '46', '3263 Logan Circle', '376-666-2455', 'hfontenot4@npr.org', '', '', NULL, '2024-08-20 20:59:51'),
(10, 'Alysia', 'Bowdidge', '26', '463 Melvin Road', '202-976-9031', 'abowdidge5@csmonitor.com', '', '', NULL, '2024-08-20 20:59:51'),
(11, 'Jacquie', 'Castellucci', '49', '94 Anthes Trail', '972-635-0853', 'jcastellucci6@trellian.com', '', '', NULL, '2024-08-20 20:59:51'),
(12, 'Travers', 'Gennings', '47', '4544 John Wall Crossing', '542-983-1407', 'tgennings7@ed.gov', '', '', NULL, '2024-08-20 20:59:51'),
(13, 'Leta', 'Rushbrooke', '5', '5544 Shelley Street', '997-553-3328', 'lrushbrooke8@360.cn', '', '', NULL, '2024-08-20 20:59:51'),
(14, 'Sonny', 'Vanderplas', '89', '38745 Pankratz Point', '131-908-1816', 'svanderplas9@rambler.ru', '', '', NULL, '2024-08-20 20:59:51'),
(15, 'Marillin', 'Doyle', '51', '527 Vera Park', '103-401-5092', 'mdoylea@utexas.edu', '', '', NULL, '2024-08-20 20:59:51'),
(16, 'Phineas', 'Giannini', '50', '83 Daystar Parkway', '827-124-7817', 'pgianninib@ihg.com', '', '', NULL, '2024-08-20 20:59:51'),
(17, 'Abdulas', 'Vain', '86121234', '1 Birchwood Junction', '137-720-0005', 'avainc@symantec.com', '', '', NULL, '2024-08-20 20:59:51'),
(18, 'Kimberly', 'Collinette', '43', '239 Eggendart Hill', '744-159-0279', 'kcollinetted@deviantart.com', '', '', NULL, '2024-08-20 20:59:51'),
(19, 'Jenifer', 'Geibel', '2', '4 Dakota Alley', '355-411-8407', 'jgeibele@ebay.co.uk', '', '', NULL, '2024-08-20 20:59:51'),
(20, 'Egon', 'Brayn', '16', '1248 Holmberg Drive', '491-385-4404', 'ebraynf@oaic.gov.au', '', '', NULL, '2024-08-20 20:59:51'),
(21, 'Carlee', 'Maber', '28', '2065 Continental Court', '378-525-0154', 'cmaberg@icio.us', '', '', NULL, '2024-08-20 20:59:51'),
(22, 'Kacy', 'Callacher', '58', '24878 Susan Place', '883-695-8003', 'kcallacherh@oakley.com', '', '', NULL, '2024-08-20 20:59:51'),
(23, 'Janean', 'Stringfellow', '12', '24011 Gulseth Crossing', '584-268-9740', 'jstringfellowi@google.ca', '', '', NULL, '2024-08-20 20:59:51'),
(24, 'Appolonia', 'Rickson', '5', '0579 Welch Hill', '239-167-1628', 'aricksonj@java.com', '', '', NULL, '2024-08-20 20:59:51'),
(25, 'Lorant', 'Attoe', '38', '75 8th Park', '195-159-4509', 'lattoek@ucsd.edu', '', '', NULL, '2024-08-20 20:59:51'),
(26, 'Iain', 'Ryam', '80', '0336 Judy Road', '780-681-2526', 'iryaml@naver.com', '', '', NULL, '2024-08-20 20:59:51'),
(27, 'Francisca', 'O\'Henehan', '83', '53 Lukken Point', '366-836-9709', 'fohenehanm@flavors.me', '', '', NULL, '2024-08-20 20:59:51'),
(28, 'Ingunna', 'Sherred', '33', '84 Meadow Valley Point', '410-115-2871', 'isherredn@youtube.com', '', '', NULL, '2024-08-20 20:59:51'),
(29, 'Darleen', 'Ziems', '29', '39282 Forest Run Circle', '555-551-8613', 'dziemso@aol.com', '', '', NULL, '2024-08-20 20:59:51'),
(30, 'Omar', 'Braxay', '83', '8 Westridge Junction', '512-574-2439', 'obraxayp@prlog.org', '', '', NULL, '2024-08-20 20:59:51'),
(31, 'Nealson', 'Upex', '66', '324 Dayton Way', '740-104-0240', 'nupexq@infoseek.co.jp', '', '', NULL, '2024-08-20 20:59:51'),
(32, 'Nanice', 'Beldum', '62', '0622 Buhler Road', '889-988-1119', 'nbeldumr@aboutads.info', '', '', NULL, '2024-08-20 20:59:52'),
(33, 'Tannie', 'Lopes', '97', '38 Kenwood Pass', '147-668-0459', 'tlopess@mediafire.com', '', '', NULL, '2024-08-20 20:59:52'),
(34, 'Norrie', 'Farnie', '56', '18 Dapin Lane', '320-141-4513', 'nfarniet@123-reg.co.uk', '', '', NULL, '2024-08-20 20:59:52'),
(35, 'Gabriel', 'Broadberry', '88', '779 Londonderry Avenue', '459-159-2580', 'gbroadberryu@tinypic.com', '', '', NULL, '2024-08-20 20:59:52'),
(36, 'Clemmie', 'Vanyashkin', '34', '22518 Sundown Crossing', '460-839-8028', 'cvanyashkinv@nasa.gov', '', '', NULL, '2024-08-20 20:59:52'),
(37, 'Harold', 'Barney', '57', '887 Morning Alley', '467-990-0193', 'hbarneyw@bloglines.com', '', '', NULL, '2024-08-20 20:59:52'),
(38, 'Patsy', 'Fishby', '92', '6720 Karstens Lane', '633-485-2990', 'pfishbyx@ftc.gov', '', '', NULL, '2024-08-20 20:59:52'),
(39, 'Tabitha', 'Akid', '71', '906 Clemons Parkway', '947-978-7516', 'takidy@sun.com', '', '', NULL, '2024-08-20 20:59:52'),
(40, 'Gilberto', 'Licciardo', '4', '831 Corry Circle', '621-113-0881', 'glicciardoz@webeden.co.uk', '', '', NULL, '2024-08-20 20:59:52'),
(41, 'Avrom', 'Ewer', '66', '23 Birchwood Park', '714-820-0461', 'aewer10@myspace.com', '', '', NULL, '2024-08-20 20:59:52'),
(42, 'Janetta', 'Micheli', '64', '65 Clarendon Plaza', '317-865-2530', 'jmicheli11@squidoo.com', '', '', NULL, '2024-08-20 20:59:52'),
(43, 'Joelly', 'MacNeilage', '38', '91 Manley Alley', '528-102-0908', 'jmacneilage12@weather.com', '', '', NULL, '2024-08-20 20:59:52'),
(44, 'Jillayne', 'Treven', '62', '44 Barby Terrace', '879-132-9991', 'jtreven13@discovery.com', '', '', NULL, '2024-08-20 20:59:52'),
(45, 'Rusty', 'Chestnutt', '26', '25684 Park Meadow Terrace', '635-192-4230', 'rchestnutt14@csmonitor.com', '', '', NULL, '2024-08-20 20:59:52'),
(46, 'Valery', 'Pozzi', '79', '0798 Acker Parkway', '780-474-8131', 'vpozzi15@tuttocitta.it', '', '', NULL, '2024-08-20 20:59:52'),
(47, 'Kermie', 'Teasdale-Markie', '75', '3 Ilene Drive', '891-526-6878', 'kteasdalemarkie16@goodreads.com', '', '', NULL, '2024-08-20 20:59:52'),
(48, 'Jamil', 'Leedal', '41', '187 Brentwood Circle', '360-897-6775', 'jleedal17@mapquest.com', '', '', NULL, '2024-08-20 20:59:52'),
(49, 'Cori', 'Plewright', '33', '5178 Southridge Place', '355-211-2234', 'cplewright18@census.gov', '', '', NULL, '2024-08-20 20:59:52'),
(50, 'Judi', 'Kelledy', '79', '32 Heffernan Center', '445-211-3467', 'jkelledy19@shinystat.com', '', '', NULL, '2024-08-20 20:59:52'),
(51, 'Pavlov', 'Roalfe', '15', '606 Hazelcrest Junction', '978-645-4738', 'proalfe1a@goo.ne.jp', '', '', NULL, '2024-08-20 20:59:52'),
(52, 'Tripp', 'Gowar', '63', '0 Melvin Lane', '981-405-8474', 'tgowar1b@dyndns.org', '', '', NULL, '2024-08-20 20:59:52'),
(53, 'Clarke', 'Peschke', '94', '78 Kennedy Avenue', '949-191-9524', 'cpeschke1c@msn.com', '', '', NULL, '2024-08-20 20:59:52'),
(54, 'Celene', 'Krauze', '20', '1840 Warbler Street', '802-311-6185', 'ckrauze1d@blog.com', '', '', NULL, '2024-08-20 20:59:52'),
(55, 'Doralynn', 'MacFayden', '97', '49 Mallory Parkway', '521-915-3972', 'dmacfayden1e@tamu.edu', '', '', NULL, '2024-08-20 20:59:52'),
(56, 'Donella', 'Hache', '50', '08 Twin Pines Trail', '914-640-8056', 'dhache1f@webs.com', '', '', NULL, '2024-08-20 20:59:53'),
(57, 'Zared', 'Reddecliffe', '24', '634 Express Road', '647-875-9864', 'zreddecliffe1g@purevolume.com', '', '', NULL, '2024-08-20 20:59:53'),
(58, 'Red', 'Wallsworth', '47', '2 Mayer Parkway', '571-460-1919', 'rwallsworth1h@liveinternet.ru', '', '', NULL, '2024-08-20 20:59:53'),
(59, 'Brandy', 'Orrock', '34', '09729 Hagan Court', '552-772-1503', 'borrock1i@walmart.com', '', '', NULL, '2024-08-20 20:59:53'),
(60, 'Orella', 'Saltwell', '55', '1 Karstens Street', '758-588-8824', 'osaltwell1j@de.vu', '', '', NULL, '2024-08-20 20:59:53'),
(61, 'Ruttger', 'Bunyard', '11', '703 Hagan Junction', '573-647-4789', 'rbunyard1k@barnesandnoble.com', '', '', NULL, '2024-08-20 20:59:53'),
(62, 'Morie', 'Guilloud', '61', '41 Fulton Point', '662-285-4679', 'mguilloud1l@marriott.com', '', '', NULL, '2024-08-20 20:59:53'),
(63, 'Fin', 'Finden', '45', '85 Oakridge Place', '156-451-9024', 'ffinden1m@indiegogo.com', '', '', NULL, '2024-08-20 20:59:53'),
(64, 'Dew', 'Deane', '74', '95 Monument Crossing', '705-940-0401', 'ddeane1n@elegantthemes.com', '', '', NULL, '2024-08-20 20:59:53'),
(65, 'Vickie', 'Vile', '31', '30 Vermont Crossing', '803-431-7489', 'vvile1o@tuttocitta.it', '', '', NULL, '2024-08-20 20:59:53'),
(66, 'Wenda', 'Drummond', '77', '2208 Paget Court', '118-113-6230', 'wdrummond1p@over-blog.com', '', '', NULL, '2024-08-20 20:59:53'),
(67, 'Mortie', 'Grichukhin', '28', '24290 Glacier Hill Road', '680-536-5398', 'mgrichukhin1q@youtu.be', '', '', NULL, '2024-08-20 20:59:53'),
(68, 'Cesare', 'McCuaig', '22', '95 Debs Point', '364-604-6036', 'cmccuaig1r@sun.com', '', '', NULL, '2024-08-20 20:59:53'),
(69, 'Gillan', 'Cope', '65', '409 Bultman Center', '885-767-1995', 'gcope1s@tinyurl.com', '', '', NULL, '2024-08-20 20:59:53'),
(70, 'Bobby', 'Bewsy', '60', '42 Aberg Alley', '804-863-4706', 'bbewsy1t@istockphoto.com', '', '', NULL, '2024-08-20 20:59:53'),
(71, 'Ashla', 'Uc', '33', '424 Center Center', '692-147-4283', 'auc1u@java.com', '', '', NULL, '2024-08-20 20:59:53'),
(72, 'Peter', 'Hartup', '98', '48837 7th Trail', '422-756-0331', 'phartup1v@webnode.com', '', '', NULL, '2024-08-20 20:59:53'),
(73, 'Basilio', 'Wyllis', '36', '1666 Utah Drive', '674-596-3641', 'bwyllis1w@youtu.be', '', '', NULL, '2024-08-20 20:59:53'),
(74, 'Stearne', 'Rishbrook', '36', '1 Blackbird Street', '991-972-1743', 'srishbrook1x@edublogs.org', '', '', NULL, '2024-08-20 20:59:53'),
(75, 'Addie', 'Freeberne', '90', '74713 Village Pass', '464-479-3424', 'afreeberne1y@sakura.ne.jp', '', '', NULL, '2024-08-20 20:59:53'),
(76, 'Delcina', 'Summerson', '85', '13641 Anthes Avenue', '714-644-4477', 'dsummerson1z@springer.com', '', '', NULL, '2024-08-20 20:59:53'),
(77, 'Dolph', 'Tocknell', '96', '20341 Linden Alley', '531-559-6610', 'dtocknell20@geocities.jp', '', '', NULL, '2024-08-20 20:59:53'),
(78, 'Cati', 'Woodlands', '71', '7 Cascade Circle', '664-938-3659', 'cwoodlands21@sphinn.com', '', '', NULL, '2024-08-20 20:59:53'),
(79, 'Donna', 'Kolczynski', '76', '4 Garrison Way', '180-368-8789', 'dkolczynski22@biblegateway.com', '', '', NULL, '2024-08-20 20:59:53'),
(80, 'Kristo', 'Caller', '70', '64496 Mockingbird Road', '185-318-9160', 'kcaller23@state.gov', '', '', NULL, '2024-08-20 20:59:53'),
(81, 'Fanni', 'Goodman', '54', '79 Dorton Park', '872-872-6139', 'fgoodman24@usa.gov', '', '', NULL, '2024-08-20 20:59:53'),
(82, 'Tildy', 'Runnacles', '43', '68535 Sycamore Point', '692-191-2507', 'trunnacles25@domainmarket.com', '', '', NULL, '2024-08-20 20:59:53'),
(83, 'Anna-diane', 'O\'Crevan', '0', '6 Crest Line Street', '673-587-4647', 'aocrevan26@army.mil', '', '', NULL, '2024-08-20 20:59:53'),
(84, 'Cathy', 'Pabst', '91', '5251 Hansons Plaza', '982-130-2232', 'cpabst27@nydailynews.com', '', '', NULL, '2024-08-20 20:59:53'),
(85, 'Happy', 'Chettoe', '8', '73208 Crescent Oaks Road', '619-338-8010', 'hchettoe28@moonfruit.com', '', '', NULL, '2024-08-20 20:59:53'),
(86, 'Randie', 'Klugman', '97', '582 Ramsey Avenue', '829-809-4829', 'rklugman29@ning.com', '', '', NULL, '2024-08-20 20:59:53'),
(87, 'Gabrila', 'Looney', '2', '62 Dayton Junction', '652-307-8556', 'glooney2a@go.com', '', '', NULL, '2024-08-20 20:59:53'),
(88, 'Jerrilee', 'Harryman', '90', '4973 Colorado Drive', '660-928-1558', 'jharryman2b@mac.com', '', '', NULL, '2024-08-20 20:59:53'),
(89, 'Avrom', 'McWhannel', '2', '24420 Buena Vista Place', '750-780-4305', 'amcwhannel2c@scribd.com', '', '', NULL, '2024-08-20 20:59:53'),
(90, 'Erwin', 'Jerrold', '20', '42 Farwell Drive', '195-530-5972', 'ejerrold2d@disqus.com', '', '', NULL, '2024-08-20 20:59:53'),
(91, 'Gypsy', 'Hindes', '4', '8 Mcbride Junction', '617-542-8715', 'ghindes2e@macromedia.com', '', '', NULL, '2024-08-20 20:59:53'),
(92, 'Chuck', 'Pantlin', '3', '89 Veith Place', '559-625-4494', 'cpantlin2f@privacy.gov.au', '', '', NULL, '2024-08-20 20:59:53'),
(93, 'Horton', 'Hardesty', '0', '71310 Hoffman Alley', '954-270-8335', 'hhardesty2g@hexun.com', '', '', NULL, '2024-08-20 20:59:53'),
(94, 'Gus', 'Draycott', '17', '48 Fordem Terrace', '159-374-0784', 'gdraycott2h@dmoz.org', '', '', NULL, '2024-08-20 20:59:54'),
(95, 'Joyce', 'Renowden', '39', '7 Spaight Drive', '980-455-3123', 'jrenowden2i@wiley.com', '', '', NULL, '2024-08-20 20:59:54'),
(96, 'Dean', 'Laudham', '15', '08960 Green Ridge Avenue', '237-984-0294', 'dlaudham2j@privacy.gov.au', '', '', NULL, '2024-08-20 20:59:54'),
(97, 'Coleen', 'Vasyutkin', '92', '997 Hansons Trail', '866-138-5260', 'cvasyutkin2k@smugmug.com', '', '', NULL, '2024-08-20 20:59:54'),
(98, 'Ardyce', 'Rostron', '86', '606 Grayhawk Road', '588-949-1839', 'arostron2l@usatoday.com', '', '', NULL, '2024-08-20 20:59:54'),
(99, 'Buffy', 'Glowacki', '74', '947 Buena Vista Park', '472-726-6053', 'bglowacki2m@meetup.com', '', '', NULL, '2024-08-20 20:59:54'),
(100, 'Eugen', 'Egle', '42', '0813 Hanson Trail', '784-490-6175', 'eegle2n@reddit.com', '', '', NULL, '2024-08-20 20:59:54'),
(101, 'Billie', 'Rodrigo', '28', '21014 Charing Cross Road', '418-881-5002', 'brodrigo2o@guardian.co.uk', '', '', NULL, '2024-08-20 20:59:54'),
(102, 'Eran', 'Backs', '54', '6 Roxbury Point', '853-991-3378', 'ebacks2p@jugem.jp', '', '', NULL, '2024-08-20 20:59:54'),
(103, 'Caryl', 'Diloway', '10', '46 Shelley Park', '280-216-7378', 'cdiloway2q@smugmug.com', '', '', NULL, '2024-08-20 20:59:54'),
(104, 'Baudoin', 'McAlees', '77', '877 Fairfield Street', '903-539-1722', 'bmcalees2r@dedecms.com', '', '', NULL, '2024-08-20 20:59:54'),
(105, 'Admin', 'Admin', '12398765', 'San Luis', '2664271316', 'edder709@gmail.com', 'nZLrGlPuD7oeOxgsbSbPJVQINrzjOomJ602TJpITeww=', NULL, 34, '2024-10-16 15:28:57');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_inmueble`
--

CREATE TABLE `tipo_inmueble` (
  `id_tipo` int(11) NOT NULL,
  `tipo` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipo_inmueble`
--

INSERT INTO `tipo_inmueble` (`id_tipo`, `tipo`) VALUES
(1, 'Casa'),
(2, 'Departamento'),
(3, 'Galpón'),
(4, 'Cochera'),
(5, 'Terreno'),
(6, 'Local');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `id_usuario` int(11) NOT NULL,
  `nombre` varchar(255) NOT NULL,
  `apellido` varchar(255) NOT NULL,
  `avatar` varchar(1000) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `rol` enum('Administrador','Empleado') NOT NULL,
  `fecha` datetime NOT NULL DEFAULT current_timestamp(),
  `borrado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`id_usuario`, `nombre`, `apellido`, `avatar`, `email`, `password`, `rol`, `fecha`, `borrado`) VALUES
(31, 'Empleado', 'Edder', '/Uploads/avatar_31Empleado33e553c9-cc10-46ff-9f78-cbb60281467b.png', 'emple@inmo.com', 'nZLrGlPuD7oeOxgsbSbPJVQINrzjOomJ602TJpITeww=', 'Empleado', '2024-09-27 01:28:37', 0),
(32, 'Jefe', 'DotNet', '/Uploads/avatar_32Jefe9d36cec6-0ebd-451a-a698-375ec4fecd9e.png', 'dotNet@inmo.com', 'nZLrGlPuD7oeOxgsbSbPJVQINrzjOomJ602TJpITeww=', 'Administrador', '2024-09-27 01:37:12', 0),
(33, 'Empleado', 'DotNet', '/Uploads/avatar_33Empleadoecaf2d23-e693-45b5-8173-fad153605c36.png', 'empleDotNet@inmo.com', 'nZLrGlPuD7oeOxgsbSbPJVQINrzjOomJ602TJpITeww=', 'Empleado', '2024-09-27 01:57:33', 0),
(34, 'Administrador', 'Edder', '/Uploads/avatar_34Administrador67fedd29-e237-4084-8679-bb3f739e1e43.png', 'admin@inmo.com', 'nZLrGlPuD7oeOxgsbSbPJVQINrzjOomJ602TJpITeww=', 'Administrador', '2024-09-27 16:11:54', 0),
(35, 'Edder', 'Santibañez', '', 'edder709@gmail.com', 'nZLrGlPuD7oeOxgsbSbPJVQINrzjOomJ602TJpITeww=', 'Administrador', '2024-09-28 03:07:38', 1);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`id_contrato`),
  ADD KEY `id_inquilino` (`id_inquilino`),
  ADD KEY `id_inmueble` (`id_inmueble`),
  ADD KEY `fk_contrato_usuario` (`id_usuario`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`id_inmueble`),
  ADD KEY `id_propietario` (`id_propietario`),
  ADD KEY `fk_inmueble_usuario` (`id_usuario`),
  ADD KEY `fk_inmueble_tipo` (`id_tipo`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`id_inquilino`),
  ADD KEY `fk_inquilino_usuario` (`id_usuario`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`id_pago`),
  ADD KEY `id_contrato` (`id_contrato`),
  ADD KEY `fk_pago_usuario` (`id_usuario`),
  ADD KEY `id_inquilino` (`id_inquilino`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`id_propietario`),
  ADD KEY `fk_propietario_usuario` (`id_usuario`);

--
-- Indices de la tabla `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  ADD PRIMARY KEY (`id_tipo`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`id_usuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `id_contrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=123;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `id_inmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=110;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `id_inquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=112;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `id_pago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `id_propietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=106;

--
-- AUTO_INCREMENT de la tabla `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  MODIFY `id_tipo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id_usuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`id_inquilino`) REFERENCES `inquilino` (`id_inquilino`),
  ADD CONSTRAINT `contrato_ibfk_2` FOREIGN KEY (`id_inmueble`) REFERENCES `inmueble` (`id_inmueble`),
  ADD CONSTRAINT `fk_contrato_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `fk_inmueble_tipo` FOREIGN KEY (`id_tipo`) REFERENCES `tipo_inmueble` (`id_tipo`),
  ADD CONSTRAINT `fk_inmueble_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`),
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`id_propietario`) REFERENCES `propietario` (`id_propietario`);

--
-- Filtros para la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD CONSTRAINT `fk_inquilino_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`);

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `fk_pago_contrato` FOREIGN KEY (`id_contrato`) REFERENCES `contrato` (`id_contrato`),
  ADD CONSTRAINT `fk_pago_inquilino` FOREIGN KEY (`id_inquilino`) REFERENCES `inquilino` (`id_inquilino`),
  ADD CONSTRAINT `fk_pago_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`);

--
-- Filtros para la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD CONSTRAINT `fk_propietario_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
