namespace OpenSpace.Base{

    public class GenericResponse<TData>: BaseResponse{
        
        public TData Data { get; set;}

        public GenericResponse(TData data){

            this.Status = 1;
            this.Mensagem = "Sucesso";
            this.Data = data;

        }
    }
}