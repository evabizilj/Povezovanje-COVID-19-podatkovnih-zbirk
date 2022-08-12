/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     17/12/2021 11:28:11                          */
/*==============================================================*/


drop table if exists BOLNISNICA;

drop table if exists CELINA;

drop table if exists CEPIVO;

drop table if exists DATUM;

drop table if exists DRZAVA;

drop table if exists ODDELEK;

drop table if exists PREBOLELI;

drop table if exists PREMINULI;

drop table if exists REGIJA;

drop table if exists SE_DELI;

drop table if exists STAROSTNA_SKUPINA;

drop table if exists TEST;

/*==============================================================*/
/* Table: BOLNISNICA                                            */
/*==============================================================*/
create table BOLNISNICA
(
   ID_BOLNISNICA        int not null,
   IME_BOLNISNICA       varchar(1024) not null,
   primary key (ID_BOLNISNICA)
);

/*==============================================================*/
/* Table: CELINA                                                */
/*==============================================================*/
create table CELINA
(
   ID_CELINA            int not null,
   IME_CELINA           varchar(1024) not null,
   primary key (ID_CELINA)
);

/*==============================================================*/
/* Table: CEPIVO                                                */
/*==============================================================*/
create table CEPIVO
(
   ID_STAROSTNA_SKUPINA int not null,
   IME_CEPIVO           int not null,
   TIP_CEPIVO           numeric(8,0) not null,
   STEVILO_CEPLJENIH    numeric(8,0) not null,
   primary key (ID_STAROSTNA_SKUPINA)
);

/*==============================================================*/
/* Table: DATUM                                                 */
/*==============================================================*/
create table DATUM
(
   ID_STAROSTNA_SKUPINA int not null,
   ID_REGIJA            int not null,
   DATUM                date not null,
   IME_DAN              varchar(50) not null,
   primary key (ID_STAROSTNA_SKUPINA, ID_REGIJA)
);

/*==============================================================*/
/* Table: DRZAVA                                                */
/*==============================================================*/
create table DRZAVA
(
   ID_DRZAVA            int not null,
   ID_CELINA            int not null,
   IME_DRZAVA           varchar(1024) not null,
   STEVILO_PREBIVALCEV  numeric(8,0) not null,
   STEVILO_OKUZENIH     char(10) not null,
   primary key (ID_DRZAVA)
);

/*==============================================================*/
/* Table: ODDELEK                                               */
/*==============================================================*/
create table ODDELEK
(
   ID_ODDELEK           int not null,
   ID_BOLNISNICA        int not null,
   IME_ODDELEK          varchar(1024) not null,
   STEVILO_INTENZIVNA_NEGA numeric(8,0) not null,
   STEVILO_NAVADNA_NEGA numeric(8,0) not null,
   primary key (ID_ODDELEK)
);

/*==============================================================*/
/* Table: PREBOLELI                                             */
/*==============================================================*/
create table PREBOLELI
(
   ID_STAROSTNA_SKUPINA int not null,
   STEVILO_PREBOLELIH   int not null,
   primary key (ID_STAROSTNA_SKUPINA)
);

/*==============================================================*/
/* Table: PREMINULI                                             */
/*==============================================================*/
create table PREMINULI
(
   ID_STAROSTNA_SKUPINA int not null,
   STEVILO_PREMINULIH   numeric(8,0) not null,
   primary key (ID_STAROSTNA_SKUPINA)
);

/*==============================================================*/
/* Table: REGIJA                                                */
/*==============================================================*/
create table REGIJA
(
   ID_REGIJA            int not null,
   ID_DRZAVA            int not null,
   IME_REGIJA           varchar(1024) not null,
   primary key (ID_REGIJA)
);

/*==============================================================*/
/* Table: SE_DELI                                               */
/*==============================================================*/
create table SE_DELI
(
   ID_STAROSTNA_SKUPINA int not null,
   ID_BOLNISNICA        int not null,
   primary key (ID_STAROSTNA_SKUPINA, ID_BOLNISNICA)
);

/*==============================================================*/
/* Table: STAROSTNA_SKUPINA                                     */
/*==============================================================*/
create table STAROSTNA_SKUPINA
(
   ID_STAROSTNA_SKUPINA int not null,
   ID_DRZAVA            int not null,
   IME_STAROSTNA_SKUPINA varchar(1024) not null,
   primary key (ID_STAROSTNA_SKUPINA)
);

/*==============================================================*/
/* Table: TEST                                                  */
/*==============================================================*/
create table TEST
(
   ID_STAROSTNA_SKUPINA int not null,
   TIP_TEST             int not null,
   IME_TEST             varchar(1024) not null,
   STEVILO_TESTIRANIH   numeric(8,0) not null,
   REZULTAT_TEST        char(50) not null,
   primary key (ID_STAROSTNA_SKUPINA)
);

alter table CEPIVO add constraint FK_STANJE4 foreign key (ID_STAROSTNA_SKUPINA)
      references STAROSTNA_SKUPINA (ID_STAROSTNA_SKUPINA) on delete restrict on update restrict;

alter table DATUM add constraint FK_IMA_TUDI foreign key (ID_REGIJA)
      references REGIJA (ID_REGIJA) on delete restrict on update restrict;

alter table DATUM add constraint FK_JE foreign key (ID_STAROSTNA_SKUPINA)
      references STAROSTNA_SKUPINA (ID_STAROSTNA_SKUPINA) on delete restrict on update restrict;

alter table DRZAVA add constraint FK_RELATIONSHIP_1 foreign key (ID_CELINA)
      references CELINA (ID_CELINA) on delete restrict on update restrict;

alter table ODDELEK add constraint FK_JE_RAZDELJENA_NA foreign key (ID_BOLNISNICA)
      references BOLNISNICA (ID_BOLNISNICA) on delete restrict on update restrict;

alter table PREBOLELI add constraint FK_STANJE2 foreign key (ID_STAROSTNA_SKUPINA)
      references STAROSTNA_SKUPINA (ID_STAROSTNA_SKUPINA) on delete restrict on update restrict;

alter table PREMINULI add constraint FK_STANJE3 foreign key (ID_STAROSTNA_SKUPINA)
      references STAROSTNA_SKUPINA (ID_STAROSTNA_SKUPINA) on delete restrict on update restrict;

alter table REGIJA add constraint FK_VSEBUJE foreign key (ID_DRZAVA)
      references DRZAVA (ID_DRZAVA) on delete restrict on update restrict;

alter table SE_DELI add constraint FK_SE_DELI foreign key (ID_STAROSTNA_SKUPINA)
      references STAROSTNA_SKUPINA (ID_STAROSTNA_SKUPINA) on delete restrict on update restrict;

alter table SE_DELI add constraint FK_SE_DELI2 foreign key (ID_BOLNISNICA)
      references BOLNISNICA (ID_BOLNISNICA) on delete restrict on update restrict;

alter table STAROSTNA_SKUPINA add constraint FK_SESTAVLJENA_IZ foreign key (ID_DRZAVA)
      references DRZAVA (ID_DRZAVA) on delete restrict on update restrict;

alter table TEST add constraint FK_STANJE foreign key (ID_STAROSTNA_SKUPINA)
      references STAROSTNA_SKUPINA (ID_STAROSTNA_SKUPINA) on delete restrict on update restrict;

