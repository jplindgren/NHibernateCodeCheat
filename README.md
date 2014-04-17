NHibernateCodeCheat
===================

Projeto com dicas e exemplos de mapeamento e configuração nhibernate.

1. ####Configuration
  - Types of Configurations
    - xml
      + hibernate.cfg.xml
      + app.config
    - code based
      + loquacious (a partir do 3.1)
      + FluentNhibernate
  - Properties
    + ConnectionString vs ConnectionStringName(key no app.config)
    + IsolationLevel
    + Timeout
    + LogSqlInConsole(loquacius) = Show_sql(xml) <- Loga as queries sql no console dos testes.
    + UpdateBatchSize (define a quantidade de inserts em um batch)(aumenta perfomace se usado junto com hilo ou guid.comb)

2. ####Mapping
  - Ids
		+ Native (deixa o banco definir. Impede o uso de batch insert pois precisa inserir e buscar id no banco)
		+ GuidComb (não precisa fazer roundtrip no banco, pode usar lotes. Ganho de até 25% de performace)(Só suportado MsSqlServer e Oracle? Mysql nos meus testes apresentou melhora performance tb)

	- Lists - Unordered - non-unique --> map using IList
	- Set -  unordered - unique --> map using Iesi.ISet
	- Bag - Un

  - Properties
	   + Lazy --> Lazy pode ter duas estratégias de Fetch, select que acaba fazendo + de 1 chamada ao banco (default) e outer-join que faz uma query só com um left-outer.
	   + Inverse --> (fica sempre do lado da collection) O hibernate não tem como saber que os dois lados da relação entre dois objetos são a mesma relação. Essa propriedade diz ao hibernate que as duas são uma mesma relação no banco e qual "lado" da relação ignorar. 
	Senão existisse o hibernate tentaria atualizar os dois lados, indo duas vezes ao banco.
	      * Inverse = true ( diz que o mandante é o lado do objeto único (filho))
	      * Não define questão de cascade. Cascades são ortognais a isso.

3. ####Query
  - Get X Load
     + Get
        - Retorna o objeto ou retorna nulo.
        - Acessa o banco imediatamente.
     + Load
		    - retorna o objeto ou lanca uma exceção ObjectNotFoundException
		    - retorna um proxy object
		    - não precisa ir ao banco imediatamente
   
