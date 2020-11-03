export class EmpresaModel {
    public id: number;
    public nomeFantasia: string;
    public uf: string;
    public cnpj: string;

    constructor(id: number, nomeFantasia: string, uf: string, cnpj: string) {
        this.id = id;
        this.nomeFantasia = nomeFantasia;
        this.uf = uf;
        this.cnpj = cnpj;
    }
}
