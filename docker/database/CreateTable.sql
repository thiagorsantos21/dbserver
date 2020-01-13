CREATE TABLE ContaCorrente (
    ID VARCHAR(36) NOT NULL,
    Agencia varchar(5)  NOT NULL ,
    Numero varchar(14) NOT NULL ,
	Digito varchar(1) NOT NULL,
    Saldo NUMERIC(18,2)  NOT NULL,
    PRIMARY KEY (
        ID
    )
);

INSERT INTO ContaCorrente (ID,Agencia,Numero,Digito,Saldo)
VALUES('1C0A0BE8-C1ED-4CBE-BEED-D63E3521FC7F', '0001', '123456', '7', 300.0);


INSERT INTO ContaCorrente (ID,Agencia,Numero,Digito,Saldo)
VALUES('8AC6C8E9-9BC9-4376-A1D9-97697071538A', '0001', '654321', '0', 200.0);