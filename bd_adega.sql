
CREATE DATABASE  bd_genial
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Portuguese_Brazil.1252'
    LC_CTYPE = 'Portuguese_Brazil.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;
	
	
	CREATE SCHEMA IF NOT EXISTS sys
    AUTHORIZATION postgres;
	
	CREATE TABLE IF NOT EXISTS  sys.usuario_tb(
		IdUsuario serial not null primary key,
		Nome varchar(300) not null,
		UserName varchar(50) not null UNIQUE,
		Senha varchar not null,
		Email varchar(200),
		EmailConfirmed boolean,
		Tipo int not null,
		Celular varchar(30) not null,
		CelularConfirmed boolean,
		TwoFactorEnabled boolean,
		CEP varchar,
		Logradouro varchar,
		Numero integer,
		Cidade varchar,
		UF varchar,
		Complemento varchar
	);
	
	create table if not exists sys.claim_tb(
		id_claim serial not null primary key,
		descricao varchar not null,
		acesso_modulo varchar(100) not null,
		permissao_modulo varchar(100) not null
	);
	
	create table if not exists sys.user_claim_tb(
		id_claim integer not null,
		id_usuario integer not null,
		constraint id_claim_fk foreign key(id_claim)
		references sys.claim_tb(id_claim),
		constraint id_usuario_fk foreign key(id_usuario)
		references sys.usuario_tb(id_usuario)
	);
	
	
--alter table if exists sys.usuario_tb add column if not exists cep varchar;
--alter table  if exists sys.usuario_tb add column if not exists logradouro varchar;
--alter table if exists sys.usuario_tb add column if not exists numero integer default 0;
--alter table if exists sys.usuario_tb add column if not exists cidade varchar;
--alter table if exists sys.usuario_tb add column if not exists uf varchar;
--alter table if exists sys.usuario_tb add column if not exists complemento varchar;

	
	CREATE TABLE IF NOT EXISTS sys.login_tb(
		id_login serial not null primary key,
		id_usuario int not null,
		data_hora timestamp,
		CONSTRAINT id_usuario_fk FOREIGN KEY(id_usuario)
		REFERENCES sys.usuario_tb(id_usuario)	
	);
	

	CREATE TABLE IF NOT EXISTS sys.cliente_tb(
		id_cliente serial not null primary key,
		nome_cliente varchar(300) not null,
		telefone varchar(30) null,
		cep varchar null,
		endereco varchar null,
		numero integer,
		bairro varchar null,
		cidade varchar null,
		uf varchar null,
		complemento varchar null,
		email varchar,
		celular varchar null,
		cpf_cnpj bigint null,
		is_pj boolean null
	);


--alter table if exists sys.cliente_tb add column if not exists email varchar;
--alter table if exists sys.cliente_tb add column if not exists numero integer ;
--alter table if exists sys.cliente_tb add column if not exists cidade varchar null;
--alter table if exists sys.cliente_tb add column if not exists uf varchar null;
--alter table if exists sys.cliente_tb add column if not exists cep varchar null;
--alter table if exists sys.cliente_tb add column if not exists celular varchar null;
--alter table if exists sys.cliente_tb add column if not exists cpf_cnpj bigint null;
--alter table if exists sys.cliente_tb add column if not exists is_pj boolean null;
--alter table if exists sys.cliente_tb add column if not exists bairro varchar null;

	create table if not exists sys.categoria_tb(
	id_categoria serial not null primary key,
	categoria varchar
);
	
	
CREATE TABLE IF NOT EXISTS sys.produto_tb
(
    id_produto serial not null primary key,
	descricao varchar ,
    tipo_medida varchar,
    quantidade integer NOT NULL,
	quantidade_unidades integer,
    preco_bruto decimal NOT NULL,
    preco_venda DECIMAL NOT NULL,
    observacoes varchar,
	imagem_base64 varchar null,
	codigo_barras varchar null,
	data_validade timestamp null,
	id_categoria integer null,
	localizacao varchar(100) null,
	constraint id_categoria_fk foreign key (id_categoria)
	references sys.categoria_tb(id_categoria)
);


CREATE TABLE IF NOT EXISTS sys.venda_tb
(
    id_venda serial not null primary key,
    data_venda timestamp without time zone NOT NULL,
    valor_total numeric NOT NULL,
    is_pagamento_dividido boolean,
    is_prazo boolean,
	id_cliente integer not null,
	status integer not null,
	CONSTRAINT id_cliente_fk FOREIGN KEY(id_cliente)
	REFERENCES sys.cliente_tb(id_cliente)
);




CREATE TABLE IF NOT EXISTS sys.item_venda_tb
(
    id_item serial not null primary key,
    id_venda integer NOT NULL,
    id_produto integer NOT NULL,
    quantidade integer NOT NULL,
	is_cupom_emitido boolean,
	CONSTRAINT id_venda_fk FOREIGN KEY (id_venda)
	REFERENCES sys.venda_tb(id_venda),
	CONSTRAINT id_produto_fk FOREIGN KEY (id_produto)
	REFERENCES sys.produto_tb(id_produto)
);

CREATE TABLE IF NOT EXISTS sys.pagto_venda_tb
(
    id_pagto serial not null primary key,
    forma_pagto varchar NOT NULL,
    valor_pago decimal NOT NULL,
    valor_troco decimal,
    comprovante varchar,
    id_venda integer NOT NULL,
	valor_recebido decimal,
    CONSTRAINT id_venda_fk FOREIGN KEY (id_venda)
    REFERENCES sys.venda_tb (id_venda)
);	

CREATE TABLE IF NOT EXISTS sys.caixa_tb
(
   id_caixa serial not null primary key,
	data timestamp not null,
	tipo_movimento integer not null,
	id_usuario integer not null,
	valor decimal not null,
	CONSTRAINT id_caixa_fk FOREIGN KEY (id_usuario)
	REFERENCES sys.usuario_tb(id_usuario)
);	

	
	
CREATE TABLE IF NOT EXISTS sys.fornecedor_tb(
	id_fornecedor serial not null primary key,
	razao_social varchar not null,
	endereco varchar,
	cpf_cnpj bigint,
	detalhes_gerais varchar,
	is_pj boolean,	
	tipo_fornecimento integer,
	bairro varchar,
	cidade varchar,
	numero integer,
	uf varchar(2),
	complemento varchar,
	id_categoria integer,	
	constraint id_categoria_fk foreign key (id_categoria)
	references sys.categoria_tb(id_categoria)
);



create table if not exists sys.contato_fornecedor_tb(
	id_contato serial not null primary key,
	id_fornecedor integer not null,
	tipo_contato integer,
	contato varchar	,
	constraint id_fornecedor_fk foreign key (id_fornecedor)
	references sys.fornecedor_tb(id_fornecedor)	
);



create table if not exists sys.contasbanco_fornecedor_tb(	
	id_conta serial not null primary key,
	agencia integer,
	conta integer,
	digito integer,
	banco varchar,
	is_pix boolean,
	chave_pix varchar,
	tipo_chave_pix integer,
	id_fornecedor integer not null,
	constraint id_fornecedor_fk foreign key (id_fornecedor)
	references sys.fornecedor_tb(id_fornecedor)
);


create table if not exists sys.negociacao_fornecedor_tb(	
	id_negociacao serial not null primary key,
	id_fornecedor integer not null,
	id_produto integer not null,
	quantidade integer not null,
	preco_bruto decimal not null,
	valor_total decimal not null,
	desconto decimal,
	data_negociacao timestamp not null,
	constraint id_fornecedor_fk foreign key (id_fornecedor)
	references sys.fornecedor_tb(id_fornecedor),
	constraint id_produto_fk_ foreign key (id_produto)
	references sys.produto_tb(id_produto)	

);


create table if not exists sys.dados_empresa_tb
(
	id_empresa serial not null primary key,
	nome_fantasia varchar not null,
	razao_social varchar not null,
	cpf_cnpj bigint,
	inscricao_municipal varchar,
	inscricao_estadual varchar,
	emails_copia_faturas varchar,
	is_pj boolean,
	cep bigint,
	logradouro varchar,
	numero integer,
	bairro varchar(100),
	cidade varchar(100),
	uf varchar(2),
	complemento varchar(200)
);



create table if not exists sys.cupom_fiscal_tb(
	id_cupom serial not null primary key,
	id_venda integer not null,
	id_empresa integer not null,
	id_usuario integer not null,
	data_emissao timestamp not null,
	is_fiscal boolean,
	constraint id_venda_fk foreign key(id_venda)
	references sys.venda_tb(id_venda),
	constraint id_empresa_fk foreign key(id_empresa)
	references sys.dados_empresa_tb(id_empresa),
	constraint id_usuario_fk foreign key (id_usuario)
	references sys.usuario_tb(id_usuario)
);







CREATE TABLE IF NOT EXISTS sys.status_servico_tb(
	id_status serial not null primary key,
	descricao varchar(200) not null
);

	CREATE TABLE IF NOT EXISTS sys.sub_status_servico_tb(
	id_status serial not null primary key,
	descricao varchar(200) not null
);

CREATE TABLE IF NOT EXISTS sys.servico_tb(
	id_servico serial not null primary key,
	descricao varchar(300) not null,
	valor decimal not null,
	custos_preparacao decimal not null,
	observacoes varchar

);
CREATE TABLE IF NOT EXISTS sys.venda_servico_tb(
	id_venda_servico serial not null primary key,
	id_cliente integer not null,
	data_venda timestamp not null,
	data_finalizacao timestamp,
	sub_total decimal,
	desconto decimal,
	total decimal,
	status integer,
	id_usuario integer,
	CONSTRAINT id_usuario_fk foreign key(id_usuario)
	REFERENCES sys.usuario_tb(id_usuario)
);

CREATE TABLE IF NOT EXISTS sys.item_servico_tb(
	id_item_servico serial not null primary key,
	id_venda_servico bigint not null,
	id_servico bigint not null,
	id_status bigint not null,
	id_sub_status bigint not null,
	quantidade integer,
	executado_por integer,
	data_execucao timestamp,
	incluido_por integer not null,
	data_inclusao timestamp not null,
	CONSTRAINT id_venda_servico_fk foreign key(id_venda_servico)
	references sys.venda_servico_tb(id_venda_servico),
	constraint id_servico_fk foreign key(id_servico)
	references sys.servico_tb(id_servico),
	constraint id_status_fk foreign key (id_status)
	references sys.status_servico_tb(id_status),
	constraint id_sub_status_fk foreign key (id_sub_status)
	references sys.sub_status_servico_tb(id_status)	
);


create table if not exists sys.entrada_saida_caixa_tb(
	id_entrada serial not null primary key,
	id_usuario integer not null,
	tipo_movimentacao integer not null,
	valor decimal not null,
	descricao varchar (300) not null,
	data_movimentacao timestamp not null,
	constraint fk_id_usuario foreign key(id_usuario)
	references sys.usuario_tb(id_usuario)	
);
	
---End version 2.1

---Garçom Digital

create table if not exists sys.mesa_tb(
	id_mesa serial not null primary key,
	numero_mesa integer unique not null,
	status_mesa integer not null,
	localizacao varchar
);


create table if not exists sys.atendimento_tb(
	id_atendimento serial not null primary key,
	data_hora timestamp not null,
	id_usuario integer not null,
	constraint id_usuario_fk foreign key(id_usuario)
	references sys.usuario_tb(id_usuario)

);

create table if not exists sys.mesa_atendimento_tb(
	id_mesa_atendimento serial not null primary key,
	id_mesa integer not null,
	id_atendimento integer not null,
	constraint id_mesa_fk foreign key(id_mesa)
	references sys.mesa_tb(id_mesa),
	constraint id_atendimento_fk foreign key(id_atendimento)
	references sys.atendimento_tb(id_atendimento)

);

create table if not exists sys.mesa_pedido_tb(
	id_mesa_atendimento integer not null,
	id_venda integer not null,
	id_produto integer not null,	
	quantidade integer not null,
	valor decimal not null,
	observacoes_pedido varchar(300),
	constraint id_mesa_atendimento_fk foreign key(id_mesa_atendimento)
	references sys.mesa_atendimento_tb(id_mesa_atendimento),
	constraint id_venda_fk foreign key(id_venda)
	references sys.venda_tb(id_venda),
	constraint id_produto_fk foreign key(id_produto)
	references sys.produto_tb(id_produto)
);

create table if not exists sys.comanda_tb(
	numero_comanda integer unique not null
);

create table if not exists sys.comanda_pedido(
	numero_comanda integer not null,
	id_venda integer not null,
	id_produto integer not null,
	quantidade integer not null,
	valor decimal not null,
	observacoes varchar(300),
	constraint id_venda_fk foreign key(id_venda)
	references sys.venda_tb(id_venda),
	constraint id_produto_fk foreign key (id_produto)
	references sys.produto_tb(id_produto)
);



create table if not exists sys.pedido_entrega_tb(
	id_pedido_entrega serial not null primary key,
	id_usuario integer not null,
	cep varchar(50),
	logradouro varchar(200),
	numero integer,
	cidade varchar(100),
	uf varchar(2),
	complemento varchar(200),
	observacoes varchar(300)
);

create table if not exists sys.item_pedido_entrega_tb(
	id_pedido_entrega integer not null,
	id_item integer not null,
	constraint id_pedido_entrega_fk foreign key(id_pedido_entrega)
	references sys.pedido_entrega_tb(id_pedido_entrega),
	constraint id_item_fk foreign key(id_item)
	references sys.item_venda_tb(id_item)
);