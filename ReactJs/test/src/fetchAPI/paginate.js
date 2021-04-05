export default function callApi(data){
    return new Promise((resolve,reject) => {
        const url = 'http://localhost:3001/items?_page='+data+'&_limit=3';
        fetch(url,{
            method:'GET'
        })
            .then((response)=>response.json())
            .then((res) => {
                resolve(res);
                console.log(res);
            })
            .catch((error)=>{
                reject(error);
            });
    });
}