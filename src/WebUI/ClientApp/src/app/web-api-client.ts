/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.12.1.0 (NJsonSchema v10.4.6.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

export interface IShipsClient {
    get(page: number | undefined, size: number | undefined, version: string): Observable<OperationListResponseOfShipDto>;
    create(version: string, command: CreateShipCommand): Observable<OperationResponseOfShipDto>;
    get2(id: number, version: string): Observable<OperationResponseOfShipDto>;
    update(id: number, version: string, command: UpdateShipCommand): Observable<void>;
    delete(id: number, version: string): Observable<void>;
}

@Injectable({
    providedIn: 'root'
})
export class ShipsClient implements IShipsClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    get(page: number | undefined, size: number | undefined, version: string): Observable<OperationListResponseOfShipDto> {
        let url_ = this.baseUrl + "/api/v{version}/Ships?";
        if (version === undefined || version === null)
            throw new Error("The parameter 'version' must be defined.");
        url_ = url_.replace("{version}", encodeURIComponent("" + version));
        if (page === null)
            throw new Error("The parameter 'page' cannot be null.");
        else if (page !== undefined)
            url_ += "page=" + encodeURIComponent("" + page) + "&";
        if (size === null)
            throw new Error("The parameter 'size' cannot be null.");
        else if (size !== undefined)
            url_ += "size=" + encodeURIComponent("" + size) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGet(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGet(<any>response_);
                } catch (e) {
                    return <Observable<OperationListResponseOfShipDto>><any>_observableThrow(e);
                }
            } else
                return <Observable<OperationListResponseOfShipDto>><any>_observableThrow(response_);
        }));
    }

    protected processGet(response: HttpResponseBase): Observable<OperationListResponseOfShipDto> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = OperationListResponseOfShipDto.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status === 400) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = ValidationProblemDetails.fromJS(resultData400);
            return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            }));
        } else if (status === 401) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result401: any = null;
            let resultData401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result401 = ProblemDetails.fromJS(resultData401);
            return throwException("A server side error occurred.", status, _responseText, _headers, result401);
            }));
        } else if (status === 500) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result500: any = null;
            let resultData500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result500 = ProblemDetails.fromJS(resultData500);
            return throwException("A server side error occurred.", status, _responseText, _headers, result500);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null);
    }

    create(version: string, command: CreateShipCommand): Observable<OperationResponseOfShipDto> {
        let url_ = this.baseUrl + "/api/v{version}/Ships";
        if (version === undefined || version === null)
            throw new Error("The parameter 'version' must be defined.");
        url_ = url_.replace("{version}", encodeURIComponent("" + version));
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processCreate(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processCreate(<any>response_);
                } catch (e) {
                    return <Observable<OperationResponseOfShipDto>><any>_observableThrow(e);
                }
            } else
                return <Observable<OperationResponseOfShipDto>><any>_observableThrow(response_);
        }));
    }

    protected processCreate(response: HttpResponseBase): Observable<OperationResponseOfShipDto> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = OperationResponseOfShipDto.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status === 400) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = ValidationProblemDetails.fromJS(resultData400);
            return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            }));
        } else if (status === 401) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result401: any = null;
            let resultData401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result401 = ProblemDetails.fromJS(resultData401);
            return throwException("A server side error occurred.", status, _responseText, _headers, result401);
            }));
        } else if (status === 500) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result500: any = null;
            let resultData500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result500 = ProblemDetails.fromJS(resultData500);
            return throwException("A server side error occurred.", status, _responseText, _headers, result500);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null);
    }

    get2(id: number, version: string): Observable<OperationResponseOfShipDto> {
        let url_ = this.baseUrl + "/api/v{version}/Ships/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        if (version === undefined || version === null)
            throw new Error("The parameter 'version' must be defined.");
        url_ = url_.replace("{version}", encodeURIComponent("" + version));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGet2(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGet2(<any>response_);
                } catch (e) {
                    return <Observable<OperationResponseOfShipDto>><any>_observableThrow(e);
                }
            } else
                return <Observable<OperationResponseOfShipDto>><any>_observableThrow(response_);
        }));
    }

    protected processGet2(response: HttpResponseBase): Observable<OperationResponseOfShipDto> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = OperationResponseOfShipDto.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status === 400) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = ValidationProblemDetails.fromJS(resultData400);
            return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            }));
        } else if (status === 401) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result401: any = null;
            let resultData401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result401 = ProblemDetails.fromJS(resultData401);
            return throwException("A server side error occurred.", status, _responseText, _headers, result401);
            }));
        } else if (status === 404) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result404: any = null;
            let resultData404 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
            }));
        } else if (status === 500) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result500: any = null;
            let resultData500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result500 = ProblemDetails.fromJS(resultData500);
            return throwException("A server side error occurred.", status, _responseText, _headers, result500);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null);
    }

    update(id: number, version: string, command: UpdateShipCommand): Observable<void> {
        let url_ = this.baseUrl + "/api/v{version}/Ships/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        if (version === undefined || version === null)
            throw new Error("The parameter 'version' must be defined.");
        url_ = url_.replace("{version}", encodeURIComponent("" + version));
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("put", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processUpdate(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processUpdate(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processUpdate(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status === 400) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = ValidationProblemDetails.fromJS(resultData400);
            return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            }));
        } else if (status === 401) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result401: any = null;
            let resultData401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result401 = ProblemDetails.fromJS(resultData401);
            return throwException("A server side error occurred.", status, _responseText, _headers, result401);
            }));
        } else if (status === 404) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result404: any = null;
            let resultData404 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
            }));
        } else if (status === 500) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result500: any = null;
            let resultData500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result500 = ProblemDetails.fromJS(resultData500);
            return throwException("A server side error occurred.", status, _responseText, _headers, result500);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null);
    }

    delete(id: number, version: string): Observable<void> {
        let url_ = this.baseUrl + "/api/v{version}/Ships/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        if (version === undefined || version === null)
            throw new Error("The parameter 'version' must be defined.");
        url_ = url_.replace("{version}", encodeURIComponent("" + version));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
            })
        };

        return this.http.request("delete", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processDelete(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processDelete(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processDelete(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status === 400) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = ValidationProblemDetails.fromJS(resultData400);
            return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            }));
        } else if (status === 401) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result401: any = null;
            let resultData401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result401 = ProblemDetails.fromJS(resultData401);
            return throwException("A server side error occurred.", status, _responseText, _headers, result401);
            }));
        } else if (status === 404) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result404: any = null;
            let resultData404 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
            }));
        } else if (status === 500) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result500: any = null;
            let resultData500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result500 = ProblemDetails.fromJS(resultData500);
            return throwException("A server side error occurred.", status, _responseText, _headers, result500);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null);
    }
}

export class OperationResponseOfIReadOnlyCollectionOfShipDto implements IOperationResponseOfIReadOnlyCollectionOfShipDto {
    payload?: ShipDto[] | undefined;
    errors?: string[] | undefined;

    constructor(data?: IOperationResponseOfIReadOnlyCollectionOfShipDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            if (Array.isArray(_data["payload"])) {
                this.payload = [] as any;
                for (let item of _data["payload"])
                    this.payload!.push(ShipDto.fromJS(item));
            }
            if (Array.isArray(_data["errors"])) {
                this.errors = [] as any;
                for (let item of _data["errors"])
                    this.errors!.push(item);
            }
        }
    }

    static fromJS(data: any): OperationResponseOfIReadOnlyCollectionOfShipDto {
        data = typeof data === 'object' ? data : {};
        let result = new OperationResponseOfIReadOnlyCollectionOfShipDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.payload)) {
            data["payload"] = [];
            for (let item of this.payload)
                data["payload"].push(item.toJSON());
        }
        if (Array.isArray(this.errors)) {
            data["errors"] = [];
            for (let item of this.errors)
                data["errors"].push(item);
        }
        return data; 
    }
}

export interface IOperationResponseOfIReadOnlyCollectionOfShipDto {
    payload?: ShipDto[] | undefined;
    errors?: string[] | undefined;
}

export class OperationListResponseOfShipDto extends OperationResponseOfIReadOnlyCollectionOfShipDto implements IOperationListResponseOfShipDto {
    meta?: MetaListResult | undefined;

    constructor(data?: IOperationListResponseOfShipDto) {
        super(data);
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.meta = _data["meta"] ? MetaListResult.fromJS(_data["meta"]) : <any>undefined;
        }
    }

    static fromJS(data: any): OperationListResponseOfShipDto {
        data = typeof data === 'object' ? data : {};
        let result = new OperationListResponseOfShipDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["meta"] = this.meta ? this.meta.toJSON() : <any>undefined;
        super.toJSON(data);
        return data; 
    }
}

export interface IOperationListResponseOfShipDto extends IOperationResponseOfIReadOnlyCollectionOfShipDto {
    meta?: MetaListResult | undefined;
}

export class MetaListResult implements IMetaListResult {
    page?: number;
    totalPages?: number;
    totalCount?: number;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;

    constructor(data?: IMetaListResult) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.page = _data["page"];
            this.totalPages = _data["totalPages"];
            this.totalCount = _data["totalCount"];
            this.hasPreviousPage = _data["hasPreviousPage"];
            this.hasNextPage = _data["hasNextPage"];
        }
    }

    static fromJS(data: any): MetaListResult {
        data = typeof data === 'object' ? data : {};
        let result = new MetaListResult();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["page"] = this.page;
        data["totalPages"] = this.totalPages;
        data["totalCount"] = this.totalCount;
        data["hasPreviousPage"] = this.hasPreviousPage;
        data["hasNextPage"] = this.hasNextPage;
        return data; 
    }
}

export interface IMetaListResult {
    page?: number;
    totalPages?: number;
    totalCount?: number;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;
}

export class ShipDto implements IShipDto {
    id!: number;
    name!: string;
    code!: string;
    lengthInMetres!: number;
    widthInMetres!: number;
    createdInUtc!: Date;
    modifiedInUtc!: Date;

    constructor(data?: IShipDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.code = _data["code"];
            this.lengthInMetres = _data["lengthInMetres"];
            this.widthInMetres = _data["widthInMetres"];
            this.createdInUtc = _data["createdInUtc"] ? new Date(_data["createdInUtc"].toString()) : <any>undefined;
            this.modifiedInUtc = _data["modifiedInUtc"] ? new Date(_data["modifiedInUtc"].toString()) : <any>undefined;
        }
    }

    static fromJS(data: any): ShipDto {
        data = typeof data === 'object' ? data : {};
        let result = new ShipDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["code"] = this.code;
        data["lengthInMetres"] = this.lengthInMetres;
        data["widthInMetres"] = this.widthInMetres;
        data["createdInUtc"] = this.createdInUtc ? this.createdInUtc.toISOString() : <any>undefined;
        data["modifiedInUtc"] = this.modifiedInUtc ? this.modifiedInUtc.toISOString() : <any>undefined;
        return data; 
    }
}

export interface IShipDto {
    id: number;
    name: string;
    code: string;
    lengthInMetres: number;
    widthInMetres: number;
    createdInUtc: Date;
    modifiedInUtc: Date;
}

/** A machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807. */
export class ProblemDetails implements IProblemDetails {
    /** A URI reference [RFC3986] that identifies the problem type. This specification encourages that, when
dereferenced, it provide human-readable documentation for the problem type
(e.g., using HTML [W3C.REC-html5-20141028]).  When this member is not present, its value is assumed to be
"about:blank". */
    type?: string | undefined;
    /** A short, human-readable summary of the problem type.It SHOULD NOT change from occurrence to occurrence
of the problem, except for purposes of localization(e.g., using proactive content negotiation;
see[RFC7231], Section 3.4). */
    title?: string | undefined;
    /** The HTTP status code([RFC7231], Section 6) generated by the origin server for this occurrence of the problem. */
    status?: number | undefined;
    /** A human-readable explanation specific to this occurrence of the problem. */
    detail?: string | undefined;
    /** A URI reference that identifies the specific occurrence of the problem.It may or may not yield further information if dereferenced. */
    instance?: string | undefined;

    constructor(data?: IProblemDetails) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.type = _data["type"];
            this.title = _data["title"];
            this.status = _data["status"];
            this.detail = _data["detail"];
            this.instance = _data["instance"];
        }
    }

    static fromJS(data: any): ProblemDetails {
        data = typeof data === 'object' ? data : {};
        let result = new ProblemDetails();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["type"] = this.type;
        data["title"] = this.title;
        data["status"] = this.status;
        data["detail"] = this.detail;
        data["instance"] = this.instance;
        return data; 
    }
}

/** A machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807. */
export interface IProblemDetails {
    /** A URI reference [RFC3986] that identifies the problem type. This specification encourages that, when
dereferenced, it provide human-readable documentation for the problem type
(e.g., using HTML [W3C.REC-html5-20141028]).  When this member is not present, its value is assumed to be
"about:blank". */
    type?: string | undefined;
    /** A short, human-readable summary of the problem type.It SHOULD NOT change from occurrence to occurrence
of the problem, except for purposes of localization(e.g., using proactive content negotiation;
see[RFC7231], Section 3.4). */
    title?: string | undefined;
    /** The HTTP status code([RFC7231], Section 6) generated by the origin server for this occurrence of the problem. */
    status?: number | undefined;
    /** A human-readable explanation specific to this occurrence of the problem. */
    detail?: string | undefined;
    /** A URI reference that identifies the specific occurrence of the problem.It may or may not yield further information if dereferenced. */
    instance?: string | undefined;
}

/** A ProblemDetails for validation errors. */
export class ValidationProblemDetails extends ProblemDetails implements IValidationProblemDetails {
    /** Gets the validation errors associated with this instance of ValidationProblemDetails. */
    errors?: { [key: string]: string[]; } | undefined;

    constructor(data?: IValidationProblemDetails) {
        super(data);
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            if (_data["errors"]) {
                this.errors = {} as any;
                for (let key in _data["errors"]) {
                    if (_data["errors"].hasOwnProperty(key))
                        (<any>this.errors)![key] = _data["errors"][key] !== undefined ? _data["errors"][key] : [];
                }
            }
        }
    }

    static fromJS(data: any): ValidationProblemDetails {
        data = typeof data === 'object' ? data : {};
        let result = new ValidationProblemDetails();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (this.errors) {
            data["errors"] = {};
            for (let key in this.errors) {
                if (this.errors.hasOwnProperty(key))
                    (<any>data["errors"])[key] = this.errors[key];
            }
        }
        super.toJSON(data);
        return data; 
    }
}

/** A ProblemDetails for validation errors. */
export interface IValidationProblemDetails extends IProblemDetails {
    /** Gets the validation errors associated with this instance of ValidationProblemDetails. */
    errors?: { [key: string]: string[]; } | undefined;
}

export class OperationResponseOfShipDto implements IOperationResponseOfShipDto {
    payload?: ShipDto | undefined;
    errors?: string[] | undefined;

    constructor(data?: IOperationResponseOfShipDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.payload = _data["payload"] ? ShipDto.fromJS(_data["payload"]) : <any>undefined;
            if (Array.isArray(_data["errors"])) {
                this.errors = [] as any;
                for (let item of _data["errors"])
                    this.errors!.push(item);
            }
        }
    }

    static fromJS(data: any): OperationResponseOfShipDto {
        data = typeof data === 'object' ? data : {};
        let result = new OperationResponseOfShipDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["payload"] = this.payload ? this.payload.toJSON() : <any>undefined;
        if (Array.isArray(this.errors)) {
            data["errors"] = [];
            for (let item of this.errors)
                data["errors"].push(item);
        }
        return data; 
    }
}

export interface IOperationResponseOfShipDto {
    payload?: ShipDto | undefined;
    errors?: string[] | undefined;
}

export class CreateShipCommand implements ICreateShipCommand {
    name!: string;
    code!: string;
    lengthInMetres!: number;
    widthInMetres!: number;

    constructor(data?: ICreateShipCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.name = _data["name"];
            this.code = _data["code"];
            this.lengthInMetres = _data["lengthInMetres"];
            this.widthInMetres = _data["widthInMetres"];
        }
    }

    static fromJS(data: any): CreateShipCommand {
        data = typeof data === 'object' ? data : {};
        let result = new CreateShipCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        data["code"] = this.code;
        data["lengthInMetres"] = this.lengthInMetres;
        data["widthInMetres"] = this.widthInMetres;
        return data; 
    }
}

export interface ICreateShipCommand {
    name: string;
    code: string;
    lengthInMetres: number;
    widthInMetres: number;
}

export class UpdateShipCommand implements IUpdateShipCommand {
    id!: number;
    name!: string;
    code!: string;
    lengthInMetres!: number;
    widthInMetres!: number;

    constructor(data?: IUpdateShipCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.code = _data["code"];
            this.lengthInMetres = _data["lengthInMetres"];
            this.widthInMetres = _data["widthInMetres"];
        }
    }

    static fromJS(data: any): UpdateShipCommand {
        data = typeof data === 'object' ? data : {};
        let result = new UpdateShipCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["code"] = this.code;
        data["lengthInMetres"] = this.lengthInMetres;
        data["widthInMetres"] = this.widthInMetres;
        return data; 
    }
}

export interface IUpdateShipCommand {
    id: number;
    name: string;
    code: string;
    lengthInMetres: number;
    widthInMetres: number;
}

export class SwaggerException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isSwaggerException = true;

    static isSwaggerException(obj: any): obj is SwaggerException {
        return obj.isSwaggerException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new SwaggerException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((<any>event.target).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}