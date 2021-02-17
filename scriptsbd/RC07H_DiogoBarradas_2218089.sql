CREATE SCHEMA santashop;

CREATE TABLE santashop.criancas ( 
    crianca_id           int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
    nome                 varchar(120)  NOT NULL    ,
    idade                int UNSIGNED NOT NULL    ,
    presente_id          int UNSIGNED     ,
    comportamento_id     int UNSIGNED     ,
    CONSTRAINT unq_criancas_comportamento_id UNIQUE ( comportamento_id ) ,
    CONSTRAINT unq_criancas_presente_id UNIQUE ( presente_id ) 
 );

CREATE TABLE santashop.presentes ( 
    presente_id          int UNSIGNED NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
    nome                 varchar(120)  NOT NULL    ,
    quantidade           int UNSIGNED NOT NULL
 );

CREATE TABLE santashop.comportamento ( 
    comportamento_id     int UNSIGNED NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
    descricao            varchar(120)  NOT NULL    ,
    condicao             boolean
 );

ALTER TABLE santashop.comportamento ADD CONSTRAINT fk_comportamento_criancas FOREIGN KEY ( comportamento_id ) REFERENCES santashop.criancas( comportamento_id ) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE santashop.presentes ADD CONSTRAINT fk_presentes_criancas FOREIGN KEY ( presente_id ) REFERENCES santashop.criancas( presente_id ) ON DELETE RESTRICT ON UPDATE RESTRICT;