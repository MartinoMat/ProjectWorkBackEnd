--
-- PostgreSQL database dump
--

\restrict zyfup4kDg18U1teQOoThNTyL8UepZIL41vtPYemhN3OjC5UOvmOyePyG0b6nt7g

-- Dumped from database version 18.1
-- Dumped by pg_dump version 18.1

-- Started on 2026-03-01 13:28:46

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE "ProjectWork";
--
-- TOC entry 4939 (class 1262 OID 24584)
-- Name: ProjectWork; Type: DATABASE; Schema: -; Owner: -
--

CREATE DATABASE "ProjectWork" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Italian_Italy.1252';


\unrestrict zyfup4kDg18U1teQOoThNTyL8UepZIL41vtPYemhN3OjC5UOvmOyePyG0b6nt7g
\connect "ProjectWork"
\restrict zyfup4kDg18U1teQOoThNTyL8UepZIL41vtPYemhN3OjC5UOvmOyePyG0b6nt7g

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4940 (class 0 OID 0)
-- Dependencies: 4939
-- Name: DATABASE "ProjectWork"; Type: COMMENT; Schema: -; Owner: -
--

COMMENT ON DATABASE "ProjectWork" IS 'Database del ProjectWork';


--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: -
--

CREATE SCHEMA public;


--
-- TOC entry 4941 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: -
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 220 (class 1259 OID 65553)
-- Name: CodiceComune; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."CodiceComune" (
    "CodiceCatastale" character(4) CONSTRAINT "CodiciComuni_CodiceCatastale_not_null" NOT NULL,
    "Comune" text CONSTRAINT "CodiciComuni_Comune_not_null" NOT NULL,
    "Provincia" character(2) CONSTRAINT "CodiciComuni_Provincia_not_null" NOT NULL
);


--
-- TOC entry 4942 (class 0 OID 0)
-- Dependencies: 220
-- Name: TABLE "CodiceComune"; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON TABLE public."CodiceComune" IS 'Tabella dei Codiici Catastali dei Comuni';


--
-- TOC entry 224 (class 1259 OID 163875)
-- Name: Esame; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."Esame" (
    "EsameId" integer NOT NULL,
    "RepartoId" integer NOT NULL,
    "Nome_Esame" text NOT NULL,
    "Desc_Esame" text
);


--
-- TOC entry 223 (class 1259 OID 163874)
-- Name: Esame_EsameId_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public."Esame_EsameId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4943 (class 0 OID 0)
-- Dependencies: 223
-- Name: Esame_EsameId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public."Esame_EsameId_seq" OWNED BY public."Esame"."EsameId";


--
-- TOC entry 226 (class 1259 OID 163887)
-- Name: Prenotazione; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."Prenotazione" (
    "PrenotazioneId" integer NOT NULL,
    "RepartoId" integer NOT NULL,
    "EsameId" integer NOT NULL,
    "Data" date NOT NULL,
    "Orario" time without time zone NOT NULL,
    "UserId" text,
    "Referto" boolean DEFAULT false NOT NULL
);


--
-- TOC entry 225 (class 1259 OID 163886)
-- Name: Prenotazione_PrenotazioneId_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public."Prenotazione_PrenotazioneId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4944 (class 0 OID 0)
-- Dependencies: 225
-- Name: Prenotazione_PrenotazioneId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public."Prenotazione_PrenotazioneId_seq" OWNED BY public."Prenotazione"."PrenotazioneId";


--
-- TOC entry 222 (class 1259 OID 163864)
-- Name: Reparto; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."Reparto" (
    "RepartoId" integer NOT NULL,
    "Nome_Reparto" text NOT NULL,
    "Desc_Reparto" text
);


--
-- TOC entry 221 (class 1259 OID 163863)
-- Name: Reparto_RepartoId_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public."Reparto_RepartoId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4945 (class 0 OID 0)
-- Dependencies: 221
-- Name: Reparto_RepartoId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public."Reparto_RepartoId_seq" OWNED BY public."Reparto"."RepartoId";


--
-- TOC entry 219 (class 1259 OID 24585)
-- Name: User; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."User" (
    "UserId" text CONSTRAINT "Users_IdUsers_not_null" NOT NULL,
    "Codice_Fiscale" text NOT NULL,
    "Nome" text NOT NULL,
    "Cognome" text NOT NULL,
    "Genere" character(1) NOT NULL,
    "Compleanno" date NOT NULL,
    "Com_Nascita" text NOT NULL,
    "Com_Residenza" text NOT NULL,
    "Ind_Residenza" text NOT NULL,
    "Email" text NOT NULL,
    "PasswordHash" text NOT NULL
);


--
-- TOC entry 4946 (class 0 OID 0)
-- Dependencies: 219
-- Name: TABLE "User"; Type: COMMENT; Schema: public; Owner: -
--

COMMENT ON TABLE public."User" IS 'Tabella Utenti';


--
-- TOC entry 4774 (class 2604 OID 163878)
-- Name: Esame EsameId; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Esame" ALTER COLUMN "EsameId" SET DEFAULT nextval('public."Esame_EsameId_seq"'::regclass);


--
-- TOC entry 4775 (class 2604 OID 163890)
-- Name: Prenotazione PrenotazioneId; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Prenotazione" ALTER COLUMN "PrenotazioneId" SET DEFAULT nextval('public."Prenotazione_PrenotazioneId_seq"'::regclass);


--
-- TOC entry 4773 (class 2604 OID 163867)
-- Name: Reparto RepartoId; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Reparto" ALTER COLUMN "RepartoId" SET DEFAULT nextval('public."Reparto_RepartoId_seq"'::regclass);


--
-- TOC entry 4780 (class 2606 OID 65562)
-- Name: CodiceComune CodCat_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."CodiceComune"
    ADD CONSTRAINT "CodCat_pkey" PRIMARY KEY ("CodiceCatastale");


--
-- TOC entry 4784 (class 2606 OID 163885)
-- Name: Esame Esame_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Esame"
    ADD CONSTRAINT "Esame_pkey" PRIMARY KEY ("EsameId");


--
-- TOC entry 4786 (class 2606 OID 163899)
-- Name: Prenotazione Prenotazione_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Prenotazione"
    ADD CONSTRAINT "Prenotazione_pkey" PRIMARY KEY ("PrenotazioneId");


--
-- TOC entry 4782 (class 2606 OID 163873)
-- Name: Reparto Reparto_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Reparto"
    ADD CONSTRAINT "Reparto_pkey" PRIMARY KEY ("RepartoId");


--
-- TOC entry 4778 (class 2606 OID 32800)
-- Name: User Users_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "Users_pkey" PRIMARY KEY ("UserId");


-- Completed on 2026-03-01 13:28:46

--
-- PostgreSQL database dump complete
--

\unrestrict zyfup4kDg18U1teQOoThNTyL8UepZIL41vtPYemhN3OjC5UOvmOyePyG0b6nt7g

