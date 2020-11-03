export class FornecedorModel {
    id: number;
    nome: string;
    email: string;
    documento: string;
    pessoaFisica: boolean;
    rg: string;
    nascimento: Date;
    constructor() {
        this.id= 0;
        this.nome= "";
        this.email= "";
        this.documento= "";
        this.pessoaFisica= true;
        this.rg= "";
        this.nascimento= new Date();
    }

}
